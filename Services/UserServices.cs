
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Common;
using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Extensions;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using DAL.NoneEntity;

namespace Services
{
    public interface IUserService
    {
        Task<string> Authunticate(string UserName, string password);
        Task<IList<UsersStausCnt>> GetUsersStatusCnt(string year, string month);
        Task<IList<NotarizersListItem>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(Notarizer user, string password);
        void Update(Notarizer user, string password = null);
        Task<bool> UpdateNotarizerStatus(int id, string email, short statusId);
        void Delete(int id);
        string GetCurrentUser();
        Task<bool> UploadNotarizationStamp(string userName, byte[] inputFile);
        Task<int> UploadQualificationDocs(string userName, byte[] inputFile);
        //  bool IsAuthenticated(User user, out string token);
        Task<NotarizerDTO> GetProfile(int id);
        Task<QualificationDocument> UserDocument(int docId);
    }

    public class UserService : IUserService
    {
        private AgreementsContext _context;
        private UserManager<User> _userManager;
        JWTTokenManagement _tokenManagement;

        private IMapper mapper { get; }
        private IConfiguration _config { get; }
        private IHttpContextAccessor _httpContextAccessor { get; }

        public UserService(AgreementsContext context, UserManager<User> userManager, IOptions<JWTTokenManagement> tokenManagement, IMapper mapper, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _tokenManagement = tokenManagement.Value;
            this.mapper = mapper;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUser()
        {
            var currentUser = _httpContextAccessor.HttpContext.User.Claims;

            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            // var userId =  _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            //var un = User.Identity.Name;
            //un = User.FindFirstValue(ClaimTypes.Name);
            var email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            return email ?? "UnkownUser";
        }
        public async Task<string> Authunticate(string UserName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(password))
                    return null;

                //   var user = _context.Users.SingleOrDefault(x => x.UserName == UserName);
                var user = await _userManager.FindByNameAsync(UserName);
                
                // check if UserName exists
                if (user == null)
                    return null;

                // check if password is correct
                if (await _userManager.CheckPasswordAsync(user, password))
                {

                    string token = string.Empty;
                    

                    var claim = new[]
                    {
                      new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.UserName)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Key));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var jwtToken = new JwtSecurityToken(
                        _tokenManagement.Issuer,
                        _tokenManagement.Audience,
                        claim,
                        expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                        signingCredentials: credentials
                    );
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                   

                    return token;


                 
                }

                return null;
            }
            catch (Exception ex)
            {
                var t = ex.Message;
                return null;
            }
            finally
            {
                if (_userManager != null) { _userManager.Dispose(); }
            }

        }

        public bool IsAuthenticated(User user, out string token)
        {
            try
            {
                token = string.Empty;
                // if (!_userManagementService.IsValidUser(request.Username, request.Password)) return false;
                //  var user =  await _userManager.FindByName(user.UserName);
                var claim = new[]
                {
                new Claim(ClaimTypes.Name, user.UserName)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    _tokenManagement.Issuer,
                    _tokenManagement.Audience,
                    claim,
                    expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                    signingCredentials: credentials
                );
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return true;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                token = null;
                return false;
            }
            finally
            {
                if (_userManager != null) { _userManager.Dispose(); }
            }
        }

        public async Task<IList<UsersStausCnt>>  GetUsersStatusCnt(string yr, string mnth)
        {
            try
            {
                Int16.TryParse(yr, out short year);
                Int16.TryParse(mnth, out short month);

                var yearParam = new SqlParameter("@Year", yr);
                var monthParam = new SqlParameter("@Month", mnth);
               // var result2 = _context.Notarizers.FromSqlInterpolated($@"SELECT status, Count(*) FROM Notarizers group by status").ToList();

                // DashboardData dd = await _context.Set<DashboardData>().FromSql("dbo.DashboardData @Year, @Month", yearParam, monthParam).FirstOrDefaultAsync();

                var grouped = from b in _context.Notarizers
                              group b.NotarizerId by b.Status into g
                              select new
                              {
                                  StatusId= g.Key,
                                  Count = g.Count()

                              };
                var glist = await grouped.ToListAsync();

                var result = new List<UsersStausCnt>();
                foreach (var g in glist)
                {
                    result.Add(new UsersStausCnt { StatusId = g.StatusId, Count = g.Count, Status = EnumFriendlyNames.GetDesplayName(g.StatusId) });
                }

                return result;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw new Exception(ex.Message);
            }

        }
        public async Task<IList<NotarizersListItem>> GetAll()
        {
            return await _context.Notarizers.AsNoTracking().Select(u => new NotarizersListItem
            {
                Id = u.NotarizerId,
                Name = u.FirstName + " " + u.FatherName + " " + u.FamilyName,
                Phone = u.MobilePhone,
                Status =  EnumFriendlyNames.GetDesplayName(u.Status),
                // DateApproved = u.DateApproved,
                DateApproved = DateTime.Now,
                IssuedStamp = u.IssuedStamp
            }).ToListAsync<NotarizersListItem>();
        }


        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            var u = mapper.Map<User>(user);
            return u;

        }

        public async Task<NotarizerDTO> GetProfile(int id )
        {
         
                var profile = await _context.Notarizers.Include(n => n.QulificationDocuments).Where(n => n.NotarizerId == id).FirstOrDefaultAsync();
                var NotarizerDTO = mapper.Map<NotarizerDTO>(profile);
            NotarizerDTO.City = " Tripoli";
            NotarizerDTO.Court = "South Tripoli";
            NotarizerDTO.Address = "1234 Sw Anderson Rd";
            NotarizerDTO.LandPhone = "777777777";
            NotarizerDTO.MobilePhone = "888888888";
            NotarizerDTO.CourtRegistratioNo = "Cr2323232";
                NotarizerDTO.Documents = new List<QualificationDocument>();
                foreach (var doc in profile?.QulificationDocuments)
                {
                    var d = new QualificationDocument();
                    d.Id = doc.Id;
                    d.DocumentName = doc.DocumentName;
                    d.DateUploaded = doc.DateUploaded;
                    NotarizerDTO.Documents.Add(d);
                }

                return NotarizerDTO;
            
        }

        public async Task<User> Create(Notarizer notarizer, string password)
        {
            try
            {
                // validation
                if (string.IsNullOrWhiteSpace(password))
                    throw new AppException("Password is required");

                if (_context.Users.Any(x => x.Id == notarizer.UserId))
                    throw new AppException("UserId \"" + notarizer.UserId + "\" is already taken");
                User user = new User();

                user.Email = notarizer.Email;
                user.UserName = notarizer.Email;

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    //foreach (var error in result.Errors)
                    //{
                    //    ModelState.AddModelError(string.Empty, error.Description);
                    //}

                    // return BadRequest(result.Errors);
                    return null;
                }

                result = await _userManager.AddToRoleAsync(user, "NOTARIZER");
                notarizer.UserId = user.Id;
                notarizer.Status = NotarizerStatusEnum.PendingReview;
                var not = await _context.Notarizers.AddAsync(notarizer);

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                var t = ex.Message;
                return null;
            }
            finally
            {
                if (_userManager != null) { _userManager.Dispose(); }
            }
        }

        public async void Update(Notarizer model, string password = null)
        {
            var notarizer = await _context.Notarizers.FindAsync(model.UserId);

            if (notarizer == null)
                throw new AppException("User not found");

            if (model.Email != notarizer.Email)
            {
                // UserName has changed so check if the new UserName is already taken
                if (_context.Users.Any(x => x.Id == model.UserId))
                    throw new AppException("UserName " + model.UserId + " is already taken");
            }

            // update user properties
            notarizer.FirstName = model.FirstName;
            notarizer.LastName = model.LastName;
            notarizer.Status = NotarizerStatusEnum.PendingReview;
            _context.Notarizers.Update(notarizer);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateNotarizerStatus(int id, string email, short statusId)
        {
            try
            {
                var notarizer = await _context.Notarizers.FindAsync(id);

                if (notarizer == null)
                    throw new AppException("User not found");

                notarizer.Status = (NotarizerStatusEnum)statusId;
                _context.Notarizers.Update(notarizer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UploadNotarizationStamp(string userName, byte[] inputFile)
        {
            var notarizer = await _context.Notarizers.FindAsync(16);
            
            if (notarizer == null)
                throw new AppException("User not found");
            if (notarizer.Status == NotarizerStatusEnum.Approved)
            {
                notarizer.Stamp = inputFile;
                notarizer.Status = NotarizerStatusEnum.IssuedStamp;
                _context.Notarizers.Update(notarizer);
                 await _context.SaveChangesAsync();
                return true;
            }
            return  false;
        }


        public async Task<int> UploadQualificationDocs(string userName, byte[] inputFile)
        {
            var notarizer = await _context.Notarizers.FindAsync(16);
           
            if (notarizer == null)
                throw new AppException("User not found");

            var d = new QualificationDocument();
            d.DocumentName = "University Cerficate";
            d.DocumentBody = inputFile;
            d.DateUploaded = DateTime.Now;
            // d.NotarizerId = 16;
            notarizer.QulificationDocuments.Add(d);
            notarizer.Status = NotarizerStatusEnum.PendingReview;
            _context.Notarizers.Update(notarizer);
            return await _context.SaveChangesAsync();
        }


       public async Task<QualificationDocument> UserDocument(int docId = 0) 
        {
            if (docId > 0 )
            {
                return await _context.QualificationDocuments.FindAsync(docId);
            }
            return null;
        }
        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}



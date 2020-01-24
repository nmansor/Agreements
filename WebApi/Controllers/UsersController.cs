


namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using System.IdentityModel.Tokens.Jwt;

    using Microsoft.Extensions.Options;

    using Microsoft.AspNetCore.Authorization;


    using DAL.Entities;
    using Services;
    using Common;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using WebApi.filters;
    using WebApi.ViewModels;
    using Services.DTO;

    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]

    // [Route("api/[controller]")]
    [ApiController]

    //   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [CustomExceptionFilterAttribute]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private IOptions<JWTTokenManagement> _tokenManagement;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //  private readonly AppSettings _appSettings;

        public UsersController(
                IUserService userService,
                IMapper mapper,
                IOptions<JWTTokenManagement> tokenManagment,
                IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenManagement = tokenManagment;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            string token = await _userService.Authunticate(model.UserName, model.Password);

            if (token == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            


          //  return Ok(new { token = token, expiration = jwtTToken.ValidTo });
            return Ok(new { token = token });
        }

       
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody]LoginViewModel model)
        {
            //await _httpContextAccessor;
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            // map dto to entity
            // var user = _mapper.Map<User>(model);

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Validation error" });
                }

                var notorizer = new Notarizer(); // _mapper.Map<Notarizer>(model);
                notorizer.Email = model.Email;
                notorizer.FirstName = model.FirstName;
                notorizer.FatherName = model.FatherName;
                notorizer.FamilyName = model.FamilyName;
                notorizer.MobilePhone = model.MobilePhone;

                var user = await _userService.Create(notorizer, model.Password);

                return Ok(user);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<ActionResult> Index([FromQuery] string year, string month, string status, string sortOrder, string pageNumber, string pageSize)
        {
            if (string.IsNullOrEmpty(status))
            {
                status = "All";
            }

            short pgNo = 0;
            short pgSize = 10;
            short.TryParse(pageSize, out pgSize);
            short.TryParse(pageNumber, out pgNo);

            var dashboard = await _userService.GetUsersStatusCnt(year, month);
            PaginationListVM<NotarizersListItem> result = new PaginationListVM<NotarizersListItem>();

            result.List = await _userService.GetAll();

            return Ok(result);
        }

      
        [HttpGet("Dashboard")]
        public async Task<ActionResult> Dashboard([FromQuery] string year, string month)
        {

            var dashboard = await _userService.GetUsersStatusCnt(year, month);
            PaginationListVM<NotarizersListItem> result = new PaginationListVM<NotarizersListItem>();

            //result.List = await _userService.GetAll();


            return Ok(dashboard);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDTO>(user);
            return Ok(userDto);
        }

        //   [Authorize]
        [HttpPut("update")]
        public IActionResult Update(int id, [FromBody]NotarizerDTO notarizerDTO)
        {
            // map dto to entity and set id
            var notarizer = _mapper.Map<Notarizer>(notarizerDTO);

            try
            {
                // save 
                _userService.Update(notarizer);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus( UpdateStatusParam p)
        {
            try
            {
                // save 
                 await _userService.UpdateNotarizerStatus(p.NotarizerId, p.NotarizerEmail, p.StatusId);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("UploadNotarizationStamp")]
        public async Task<IActionResult> Post(IList<IFormFile> files)
        {
            // if (Request.Form.Files.Count > 0)
            // {
            //  var files = Request.Form.Files[0];
            if (files?.Count > 0)
            {
                using (var s = new MemoryStream())
                {
                    var doc = new Document();
                    await files[0].CopyToAsync(s);
                       await  _userService.UploadNotarizationStamp("nm@yahoo.com", s.ToArray());
                    return Ok();
                }
            }
            //}
            return BadRequest(new { message = "No file uploaded" });
        }

        [HttpPost("UploadQualificationDoc")]
        public async Task<IActionResult> UploadQualificationDoc(IList<IFormFile> files)
        {
            // if (Request.Form.Files.Count > 0)
            // {
            //  var files = Request.Form.Files[0];
            if (files?.Count > 0)
            {
                using (var s = new MemoryStream())
                {
                    var doc = new Document();
                    await files[0].CopyToAsync(s);
                    await _userService.UploadQualificationDocs("nm@yahoo.com", s.ToArray());
                    return Ok();
                }
            }
            //}
            return BadRequest(new { message = "No file uploaded" });
        }

        [HttpGet("GetProfile/{id}")]
        public async Task<IActionResult> GetProfile(int id = 0)
        {

            if (id == 0)
            {
                var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            }
             var profile = await _userService.GetProfile(id);

            return Ok(profile);
        }

        [HttpGet("ViewDocument/{id}")]
        public async Task<IActionResult> ViewDocument(int id)
        {
            var doc = await _userService.UserDocument(id);
            MemoryStream ms = new MemoryStream(doc.DocumentBody);
            return new FileStreamResult(ms, "application/pdf");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        //     public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        //     {
        //         var claims = new[]
        //         {
        //   new Claim(JwtRegisteredClaimNames.Sub, userName),
        //   new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        //   new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64), identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
        //   identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
        //};

        //         // Create the JWT security token and encode it.
        //         var jwt = new JwtSecurityToken(
        //             issuer: _jwtOptions.Issuer,
        //             audience: _jwtOptions.Audience,
        //             claims: claims,
        //             notBefore: _jwtOptions.NotBefore,
        //             expires: _jwtOptions.Expiration,
        //             signingCredentials: _jwtOptions.SigningCredentials);

        //         var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        //         return encodedJwt;
        //     }
    }

}

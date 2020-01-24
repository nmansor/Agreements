using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Cors;
using System.IO;
using System;

using Microsoft.AspNetCore.Hosting;

using WebApi.filters;
using Services.DTO;
using Newtonsoft.Json.Linq;
using WebApi.ViewModels;
using Newtonsoft.Json;
using Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;

namespace WebApi.Controllers
{
   // [Produces("application/json")]
    [EnableCors("CorsPolicy")]

    [Route("api/[controller]")]
    [ApiController]

    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [CustomExceptionFilterAttribute]
    public class AgreementController : ControllerBase
    {

        private readonly IAgreementService _service;
        private readonly IMapper _mapper;
        private readonly IOptions<Common.JWTTokenManagement> _tokenManagement;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostingEnvironment;
       
        private string webRootPath;
        private string contentRootPath;
        public AgreementController(
                IWebHostEnvironment hostingEnvironment,
                IAgreementService agreementService,
                
                IMapper mapper,
                IOptions<Common.JWTTokenManagement> tokenManagment,
             IUserService userService
            )
                
        {
            _hostingEnvironment = hostingEnvironment;
            _service = agreementService;
            
            _mapper = mapper;
            _tokenManagement = tokenManagment;
            _userService = userService;
            webRootPath = _hostingEnvironment.WebRootPath;
            contentRootPath = hostingEnvironment.ContentRootPath;

            //   ArabicFont.GetArabicFont();
        }
        public async Task<ActionResult> Index([FromQuery]  string year, string month, string status, string sortOrder, string pageNumber, string pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(status) ) {
                    status = "All";
                }

                short pgNo = 0;
                short pgSize = 10;
                short.TryParse(pageSize, out pgSize);
                short.TryParse(pageNumber, out pgNo);
                PaginationListVM<AgreementListItem> result = new PaginationListVM<AgreementListItem>();

                result.TotalRecs = await _service.GetAgrrementsCnt(16,  year,  month);
                var currentUser = _userService.GetCurrentUser();
               
               // var user = await _mapper.FindByEmailAsync(email);

                result.List = await _service.GetAgrrements(16,  year, month, status);
                //result.List = await _service.GetAgrrements(1, year, month, sortOrder, pageNumber, pageSize);

                 return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("Counts")]
        public async Task<IActionResult> GetAgreementsCounts(string year, string month)
        {
             var data = await _service.GetAgreementsCounts(16);
            return Ok(data);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var result =  await _service.GetAgrrements(2);
        //    PaginationListVM<AgreementListItem> data = new PaginationListVM<AgreementListItem>();
        //    data.List = result;
        //    data.TotalRecs = result.Count;

            //    return Ok(data);
            //}

        [HttpGet("ViewDocument/{id}")]
        public async Task<IActionResult> ViewDocument(long id)
        {
            var doc = await _service.GetAgrrement(id);
            // string file = $"{contentRootPath}/Templates/توكيل خاص.pdf";
            //var iStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);

            // return File(iStream, "application/pdf");

            MemoryStream ms = new MemoryStream(doc.Document.DocumentBody);
            return new FileStreamResult(ms, "application/pdf");

        }

        [Microsoft.AspNetCore.Mvc.HttpPost("UploadStream")]
        [AllowAnonymous]
        public async Task<IActionResult> Post()
        {
            byte[] pdfDoc;
            try
            {
            
               var rawQueryString = Request.QueryString.Value;
               
               var rawQueryStringKeyValue = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(rawQueryString);
           
               var data =   rawQueryStringKeyValue["DocModel"];
               var srcDoc = rawQueryStringKeyValue["DocumentName"];
               var paramList = data[0].Substring(1, data[0].Length-2).Replace("\"","").Split(',');
                Dictionary<string, string> list = new Dictionary<string, string>();
              
                foreach (var t in paramList)
                {
                    var tt = t.Split(':');
                    list.Add(tt[0],tt[1]);
                }
                
                string file = $"{contentRootPath}/src/assets/templates/"+ srcDoc;
                file = file.Replace("WebApi", "webApp");
            
                string outPutPath =  $"{contentRootPath}/Documents/"+ srcDoc;
                _service.WritePDF(file, list, outPutPath);
             
                return Ok();
                
            }
            catch(Exception ex)
            {
                string m = ex.Message;
                return BadRequest(new { message = ex.Message });
            }
            
        
          //  return BadRequest(new { message = "No file uploaded" });
        }


        [Microsoft.AspNetCore.Mvc.HttpPost("GeneratePdfDoc")]
        [AllowAnonymous]
        public IActionResult GeneratePdfDoct()
        {
            
            try
            {
                var rawQueryString = Request.QueryString.Value;

                var rawQueryStringKeyValue = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(rawQueryString);

                var data = rawQueryStringKeyValue["DocModel"];
                var srcDoc = rawQueryStringKeyValue["DocumentName"];
                var paramList = data[0].Substring(1, data[0].Length - 2).Replace("\"", "").Split(',');
                Dictionary<string, string> list = new Dictionary<string, string>();

                foreach (var t in paramList)
                {
                    var tt = t.Split(':');
                    list.Add(tt[0], tt[1]);
                }

                //  string file = $"{contentRootPath}/Templates/طلب _انسحاب _من _ شركة.pdf";
                string file = $"{contentRootPath}/src/assets/templates/" + srcDoc;
                file = file.Replace("WebApi", "AppClient");

                //using (var ms = new MemoryStream())
                //{

                //  await Request.Body.CopyToAsync(ms);
                //  var doc =  ReadPdfDocument.ReadPdfFile(contentRootPath);
                //  pdfDoc = _service.AddPdfAgrrement(ms.ToArray(), list);

                //  //d.DocumentBody = ms.ToArray();
                //}

               

                var filename = $"{Guid.NewGuid().ToString()}.pdf";

                var generatedDoc = _service.GeneratePDF(file, list);
                Response.Headers["Content-Disposition"] = $"inline; filename={filename}";

                // return File(generatedDoc )
             
               // return File(generatedDoc, "application/pdf");

                MemoryStream ms = new MemoryStream(generatedDoc);
                return new FileStreamResult(ms, "application/pdf");

                //var fileContentResult = new FileContentResult(generatedDoc, "application/pdf")
                //{
                //    FileDownloadName = $"{filename}"
                //};


                //// I need to delete file after me
                //// System.IO.File.Delete(filename);
                //return fileContentResult;

            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return BadRequest(new { message = ex.Message });
            }


            //  return BadRequest(new { message = "No file uploaded" });
        }


        [HttpPost("ReViewAgreement")]
        public IActionResult ReViewAgreement( Object q )
        {

            try
            {

                Dictionary<string, string> paramList = JsonConvert.DeserializeObject<Dictionary<string, string>>(q.ToString());

                var srcDocName = paramList["agreementFormName"];

                string file = $"{contentRootPath}/src/assets/templates/" + srcDocName;
                file = file.Replace("WebApi", "AppClient");

                var filename = $"{Guid.NewGuid().ToString()}.pdf";

                var generatedDoc = _service.GeneratePDF(file, paramList);
                Response.Headers["Content-Disposition"] = $"inline; filename={filename}";


                MemoryStream ms = new MemoryStream(generatedDoc);
                return new FileStreamResult(ms, "application/pdf");

                //var fileContentResult = new FileContentResult(generatedDoc, "application/pdf")
                //{
                //    FileDownloadName = $"{filename}"
                //};


                //// I need to delete file after me
                //// System.IO.File.Delete(filename);
                //return fileContentResult;

            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return BadRequest(new { message = ex.Message });
            }


            //  return BadRequest(new { message = "No file uploaded" });
        }

        [HttpPost("SaveAgreement")]
        public IActionResult SaveAgreement(Object q)
        {

            try
            {

                Dictionary<string, string> paramList = JsonConvert.DeserializeObject<Dictionary<string, string>>(q.ToString());

                var srcDocName = paramList["agreementFormName"];

                string file = $"{contentRootPath}/src/assets/templates/" + srcDocName;
                file = file.Replace("WebApi", "AppClient");

                var filename = $"{Guid.NewGuid().ToString()}.pdf";

                var generatedDoc = _service.GeneratePDF(file, paramList);
                Response.Headers["Content-Disposition"] = $"inline; filename={filename}";


                MemoryStream ms = new MemoryStream(generatedDoc);
                return new FileStreamResult(ms, "application/pdf");

                //var fileContentResult = new FileContentResult(generatedDoc, "application/pdf")
                //{
                //    FileDownloadName = $"{filename}"
                //};


                //// I need to delete file after me
                //// System.IO.File.Delete(filename);
                //return fileContentResult;

            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return BadRequest(new { message = ex.Message });
            }


            //  return BadRequest(new { message = "No file uploaded" });
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("SaveDocumentModel")]
        public async Task<IActionResult> SaveDocumentModel(CompanyInitiationDTO model)
        {
            byte[] pdfDoc;
            try
            {


                string srcDoc = "CompanyInitiation.pdf";
                Dictionary<string, string> list = new Dictionary<string, string>();

                //  string file = $"{contentRootPath}/Templates/طلب _انسحاب _من _ شركة.pdf";
                string file = $"{contentRootPath}/src/assets/templates/" + srcDoc;
                file = file.Replace("WebApi", "webApp");
                //using (var ms = new MemoryStream())
                //{

                //  await Request.Body.CopyToAsync(ms);
                //  var doc =  ReadPdfDocument.ReadPdfFile(contentRootPath);
                //  pdfDoc = _service.AddPdfAgrrement(ms.ToArray(), list);

                //  //d.DocumentBody = ms.ToArray();
                //}

                //  _service.GeneratePDF(file, list);

                string outPutPath = $"{contentRootPath}/Documents/" + srcDoc;
            //    _service.MapPDFDocumentParams(file, outPutPath, model);
                //    _pdfDocumentService.Filldata(file, outPutPath, list);

                var filename = $"{Guid.NewGuid().ToString()}.pdf";

                ////  Response.Headers["Content-Disposition"] = $"inline; filename={filename}";
                //var fileContentResult = new FileContentResult(pdfDoc, "application/pdf")
                //{
                //    FileDownloadName = $"{filename}"
                //};

                // I need to delete file after me
                // System.IO.File.Delete(filename);

                return Ok();


            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return BadRequest(new { message = ex.Message });
            }


            //  return BadRequest(new { message = "No file uploaded" });
        }

        private IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }

        [HttpGet]
        [Route("GetMyPdf")]
        public IActionResult GetMyPdf()
        {
            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/pdfjs/web/gre_research_validity_data.pdf");
            byte[] bytes = System.IO.File.ReadAllBytes(pdfPath);
            return File(bytes, "application/pdf");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if  (id == 0)
            {
                return BadRequest();
            }
            var doc = await _service.GetAgrrement(id);
            if (doc == null)
            {
                return BadRequest();
            }
             // await _service.DeleteAgreement(doc);
            return Ok();

        }
    }
}
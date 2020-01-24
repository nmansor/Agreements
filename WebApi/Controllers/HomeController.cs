
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Services;
using Services.DTO;

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.filters;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [CustomExceptionFilterAttribute]
    public class HomeController : ControllerBase
    {
        
        private readonly string webRootPath;
        private readonly string contentRootPath;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAgreementService _service;

       
        public HomeController(IWebHostEnvironment hostingEnvironment, IAgreementService service, IHttpContextAccessor httpContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _service = service;
             webRootPath = hostingEnvironment.WebRootPath;
             contentRootPath = hostingEnvironment.ContentRootPath;
            _httpContextAccessor = httpContext;
        }



        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {

               //  PdfDocument document = new PdfDocument();
                string file = $"{contentRootPath}/Templates/طلب _انسحاب _من _ شركة.docx";
                string tofile = $"{contentRootPath}/Templates/طلب _انسحاب _من _ شركة.PDF";
               // document.LoadFromFile(file);
              //Convert Word to PDF
             // document.SaveToFile(tofile, FileFormat.PDF);
         



            //Launch Document
            System.Diagnostics.Process.Start("toPDF.PDF");

                var result = await _service.GetAgrrements(2);

                if (!result.Any())
                    return NotFound();
              //  var r = query.Skip(pgNo * pgSize).Take(pgSize).ToList();
                PaginationListVM<AgreementListItem> data = new PaginationListVM<AgreementListItem>();
                data.List = result;
                data.TotalRecs = result.Count();
              
                return Ok(data);
            }

            catch (System.Exception ex)
            {
                //  Serilog.Log.Error($"ReportController.CreatePDF() - {ex}");
                return (new JsonResult(new { isError = true, errorMessage = ex.Message }));
            }
        }

        //[HttpPost]
        //public (JsonResult, IActionResult) CreatePDF([FromBody] Agreement model)
        //{
        //    try
        //    {
        //        return (new JsonResult(new { isError = false }), GetDocument());
        //    }

        //    catch (System.Exception ex)
        //    {
        //      //  Serilog.Log.Error($"ReportController.CreatePDF() - {ex}");
        //        return (new JsonResult(new { isError = true, errorMessage = ex.Message }), null);
        //    }
        //}

        //// [HttpGet("{id}")]
        //public async Task<IActionResult> Get()
        //{

        //    var result = await _service.GetAgrrements(1);
        //    if (!result.Any())
        //        return NotFound();

        //    return Ok(result);

        //}


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
           var doc = await _service.GetAgrrement(id);
            if (doc == null)
            {
                return NotFound();
            }
            // string file = $"{contentRootPath}/Templates/توكيل خاص.pdf";
            //var iStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);

            // return File(iStream, "application/pdf");

            MemoryStream ms = new MemoryStream(doc.Document.DocumentBody);
            return  new FileStreamResult(ms, "application/pdf");
            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        { 
        
        }
        //public  void ReturnPDF(byte[] contents)
        //{
        //    ReturnPDF(contents, null);
        //}

        //public  void ReturnPDF(byte[] contents, string attachmentFilename)
        //{
        //    var response = HttpContext.Current.Response;

        //    if (!string.IsNullOrEmpty(attachmentFilename))
        //        response.AddHeader("Content-Disposition", "attachment; filename=" + attachmentFilename);

        //    response.ContentType = "application/pdf";
        //    response.BinaryWrite(contents);
        //    response.End();
        //}
    }
}

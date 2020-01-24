using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using DAL.Entities;
using WebApi.ViewModels;
using WebApi.filters;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
   
    [EnableCors("CorsPolicy")]

    //  [ApiController]

    [CustomExceptionFilterAttribute]
    public class LookUpTablesController : ControllerBase
    {
        private readonly AgreementsContext _repo;
        public LookUpTablesController(AgreementsContext entitiesContext)
        {
            _repo = entitiesContext;
        }


        [Route("Cities")]
        [HttpGet]
        public IEnumerable<LookUpItemVM> Cities()
        {
            return _repo.Cities.Select(c => new LookUpItemVM { Id = c.Id, Name = c.Name }).ToList();
        }



        [Route("Courts")]
        [HttpGet]
        public IEnumerable<LookUpItemVM> Courts()
        {
            return _repo.Courts.Select(c => new LookUpItemVM { Id = c.Id, Name = c.Name }).ToList();
        }

        [Route("AgreementTypes")]
        [HttpGet]
        public IEnumerable<LookUpAgreementType> AgreementTypes()
        {
            return _repo.AgreementTypes.Select(a => new LookUpAgreementType { Id = a.Id, Name = a.Name , TemplateName = a.TemplateName}).ToList();
        }

        [Route("QualificationDocuments")]
        [HttpGet]
        public IEnumerable<LookUpItemVM> NotorizerDocumentList()
        {
            return _repo.QualificationDocumentLookup.Select(a => new LookUpItemVM { Id = a.Id, Name = a.Name  }).ToList();
        }

        [Route("UsersStatusList")]
        [HttpGet]
        public IEnumerable<LookUpItemVM> UsersStatusList()
        {
            return _repo.NotarizerStatusLookup.Select(a => new LookUpItemVM { Id = a.Id, Name = a.Status }).ToList();
        }

    }
}
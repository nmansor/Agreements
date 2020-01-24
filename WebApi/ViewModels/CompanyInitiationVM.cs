using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
   public class ParnterCompany
    {
        public string PartnerCompanyName { get;  set; }
        public string DeputyName { get; set; }
        public string DeputyIDNumber { get; set; }
    }

   public class CompanyInitiationVM
    {
        public CompanyInitiationVM()
        {
            Partners = new List<ParnterCompany>();
        }
        public int Documentd { get; set; }
        public string CompanyName { get; set; }
        public string DayOfTheWeek { get; set; }
        public string DateCreated { get; set; }
        public string NotorizerName { get; set; }
        public string NotorizerRegistrationNumber { get; set; }
        public string RegisteredCourtName { get; set; }
        public IList<ParnterCompany> Partners { get; set; }
    }
 
}

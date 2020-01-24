using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
   public  class VehicleSalesContract
    {
        public int Id { get; set; }
        public Person SellerName { get; set; }
        public Person BuyerName { get; set; }
        public string VehicleName { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleYear { get; set; }
        public string Color { get; set; }
        public string PlateNo { get; set; }
        public string PlateIssuingAgency { get; set; }
        public string VINBumber { get; set; }
        public string EngineNumber { get; set; }
        public string ManfacturingCountry { get; set; }
        public string ManfacturingDate { get; set; }
    }
}

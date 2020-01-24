using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
   public  class AgreementListItem
    {
        public long Id { get; set; }
        public  string Title { get; set; }
        public DateTime AgreementDate { get; set; }
        public byte[] AgreementBody { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string Status { get; set; }
    }
}

using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public partial class Agreement
    {
        public long AgreementId { get; set; }
        public string Hour { get; set; }
        public string Day { get; set; }
        public DateTime AgreementDate { get; set; }
        public string WakeelName { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string MuaKalName { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DocumentStatusEnum Status { get; set; }
        public short AgreementTypeId { get; set; }
        public AgreementType AgreementType { get; set; }
        public long DocumentId { get; set; }
        public virtual Document Document { get; set; }
        public int NotarizerId { get; set; }
        public virtual Notarizer Notarizer { get; set; }
    }
}

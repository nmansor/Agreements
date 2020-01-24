using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class QualificationDocument
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentBody { get; set; }
        public DateTime DateUploaded { get; set; }
        public bool Verified { get; set; }
        public int? NotarizerId { get; set; }

        public virtual Notarizer Notarizer { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Document
    {
        public Document()
        {
            Agreements = new HashSet<Agreement>();
        }

        public long DocumentId { get; set; }
        public byte[] DocumentBody { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}

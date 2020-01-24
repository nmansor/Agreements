using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AgreementTypes
    {
        public short Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
    }
}

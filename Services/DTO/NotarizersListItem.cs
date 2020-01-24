using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class NotarizersListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Phone {get; set;}
        public DateTime? DateApproved { get; set; }
        public bool IssuedStamp { get; set; }

    }
}

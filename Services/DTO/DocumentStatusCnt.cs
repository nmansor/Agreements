using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
    public class DocumentStatusCnt
    {
        public DocumentStatusEnum StatusId { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }
}

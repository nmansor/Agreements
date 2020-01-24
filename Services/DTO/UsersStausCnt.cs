using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{
   public class UsersStausCnt
    {
        public NotarizerStatusEnum StatusId { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }
}

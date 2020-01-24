using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class UpdateStatusParam
    {
        public int NotarizerId { get; set; }
        public string NotarizerEmail { get; set; }
        public short StatusId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
   
    public class PaginationListVM<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int TotalRecs { get; set; }
        public PaginationListVM()
        {
            List = new List<T>();
        }
    }
}

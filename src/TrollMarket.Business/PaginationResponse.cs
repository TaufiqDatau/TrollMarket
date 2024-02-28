using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.Business
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems {  get; set; }
        public int TotalPages { 
            get
            {
                return (int)Math.Ceiling((double)CurrentPage / TotalItems);
            } 
        }
    }
}

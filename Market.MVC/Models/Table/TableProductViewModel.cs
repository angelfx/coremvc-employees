using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.MVC.Models
{
    public class TableProductViewModel
    {
        public bool NextPage = false;

        public int Page = 1;

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}

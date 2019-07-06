using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Models.DTO
{
    public class TableProductDTO
    {
        public bool NextPage = false;

        public IEnumerable<ProductDTO> Products { get; set; }
    }
}

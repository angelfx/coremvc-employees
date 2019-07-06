using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Entities
{
    public class Unit
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}

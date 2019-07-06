using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string ShortTitle { get; set; }

        public string Title { get; set; }

        public virtual UnitDTO Unit { get; set; }

        public int Count { get; set; }

        public byte[] Image { get; set; }

        public string ImageExtension { get; set; }

        public string ImageName { get; set; }
    }
}

using System;

namespace Market.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string ShortTitle { get; set; }

        public string Title { get; set; }

        public virtual Unit Unit { get; set; }

        public int Count { get; set; }

        public byte[] Image { get; set; }

        public string ImageExtension { get; set; }

        public string ImageName { get; set; }
    }
}

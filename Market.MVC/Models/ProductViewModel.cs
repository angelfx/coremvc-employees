using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace Market.MVC.Models
{
    /// <summary>
    /// Модель представления
    /// </summary>
    public class ProductViewModel
    {
        [DisplayName("Product ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Input a short title")]
        [DisplayName("Short title")]
        public string ShortTitle { get; set; }

        [Required(ErrorMessage = "Input a full title")]
        [DisplayName("Full title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Select a unit")]
        [DisplayName("Unit")]
        public int UnitId { get; set; }

        [DisplayName("Unit")]
        public string UnitTitle { get; set; }

        [Required(ErrorMessage = "Input a quantity of product")]
        [DisplayName("Quantity of product")]
        public int Count { get; set; }

        public string ImageName { get; set; }

        public string ImageExtension { get; set; }

        [DisplayName("Image")]
        public byte[] Image { get; set; }

        
    }
}

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
    /// Расширение модели. Добавляем поля, которые нам понадобятся на форме
    /// </summary>
    public class ProductFormViewModel : ProductViewModel
    {
        [DisplayName("Image")]
        public IFormFile ImageForm { get; set; }

        /// <summary>
        /// Для вывода на форме списка выбора
        /// </summary>
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList UnitList { get; set; }

        /// <summary>
        /// Список ошибок из бэкенда
        /// </summary>
        public List<string> Errors { get; set; }
    }
}

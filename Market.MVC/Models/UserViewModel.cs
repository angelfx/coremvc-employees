using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.MVC.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}

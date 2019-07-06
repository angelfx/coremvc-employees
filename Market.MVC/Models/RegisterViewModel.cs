using System;
using System.ComponentModel.DataAnnotations;


namespace Market.MVC.Models
{
    /// <summary>
    /// Модель регистрации нового пользователя
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Input a Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Inpur a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password missmatch")]
        public string ConfirmPassword { get; set; }
    }
}

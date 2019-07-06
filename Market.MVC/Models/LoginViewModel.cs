using System.ComponentModel.DataAnnotations;

namespace Market.MVC.Models
{
    /// <summary>
    /// Модель для логина пользователя
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Input your Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Input your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

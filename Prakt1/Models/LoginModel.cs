using System.ComponentModel.DataAnnotations;

namespace Prakt1.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Логин")]

        public string Login { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]

        public string Password {get; set; }
}
}

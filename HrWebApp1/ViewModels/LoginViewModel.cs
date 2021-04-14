using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrWebApp1.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указана почта")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан Пароль")]
        public string Password { get; set; }
    }
}

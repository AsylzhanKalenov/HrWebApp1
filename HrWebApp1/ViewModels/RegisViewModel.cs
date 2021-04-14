using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrWebApp1.ViewModels
{
    public class RegisViewModel
    {
        [Required(ErrorMessage = "Не указан Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан Фамилия")]
        public string Surname { get; set; }
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Не указан Пол")]
        public int Male { get; set; }
        [Required(ErrorMessage = "Не указан Дата рождение")]
        public DateTime BirthDate { get; set; }
        [Phone]
        [Required(ErrorMessage = "Не указан номер мобильного телефона")]
        public string Phone { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Не указана почта")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}

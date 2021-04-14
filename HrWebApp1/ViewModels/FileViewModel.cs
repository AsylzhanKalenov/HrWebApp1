using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrWebApp1.ViewModels
{
    public class FileViewModel
    {
        [Required(ErrorMessage = "Не указан Удв")]
        public IFormFile[] Udv { get; set; }
        [Required(ErrorMessage = "Не указан Диплом")]
        public IFormFile[] Specialization { get; set; }
        [Required(ErrorMessage = "Не указан Пенсия")]
        public IFormFile[] Pension { get; set; }
        [Required(ErrorMessage = "Не указан Сертификат")]
        public IFormFile[] Certificates { get; set; }
        [Required(ErrorMessage = "Не указан Трудовая книжка")]
        public IFormFile[] Employhis { get; set; }
        [Required(ErrorMessage = "Не указан Адрес")]
        public IFormFile[] Addres { get; set; }
        [Required(ErrorMessage = "Не указан Справка о судимости")]
        public IFormFile[] Conviction { get; set; }
        [Required(ErrorMessage = "Не указан Нарко диспансер")]
        public IFormFile[] Narcodisp { get; set; }
        [Required(ErrorMessage = "Не указан Псих диспансер")]
        public IFormFile[] Psychodisp { get; set; }
        [Required(ErrorMessage = "Не указан Военный билет")]
        public IFormFile[] Military { get; set; }
        [Required(ErrorMessage = "Не указан Документальная фото")]
        public IFormFile[] Docphoto { get; set; }
        public IFormFile[] Refmainjob { get; set; }
        public bool IsRefmainjob { get; set; }
        public IFormFile[] Marriage { get; set; }
        public bool IsMarriage { get; set; }
        [Required(ErrorMessage = "Не указан Расчетный счет")]
        public IFormFile[] Cash { get; set; }
        [Required(ErrorMessage = "Не указан Форма 086")]
        public IFormFile[] Forma086 { get; set; }
        public int UserId { get; set; }
        public int FileCatsId { get; set; }
    }
}

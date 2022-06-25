using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhonkChief.Domain.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Неверный формат")]
        [Required(ErrorMessage ="Не заполнено поле")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Не заполнено поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

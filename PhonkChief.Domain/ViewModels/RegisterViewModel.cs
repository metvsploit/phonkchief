using System.ComponentModel.DataAnnotations;

namespace PhonkChief.Domain.ViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Не указан электронный адрес")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 символов")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Не указан никнейм")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Никнейм должен содержать от 5 символов")]
        public string NickName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}

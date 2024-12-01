using System.ComponentModel.DataAnnotations;

namespace ShopFlower.Models
{
    public class Registr
    {
        [Required(ErrorMessage = "Это поле обязательно")]
        [EmailAddress(ErrorMessage = "Введите корректный адрес электронной почты")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Логин должен быть от 5 до 40 символов")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Пароль не может быть длиннее 100 символов")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Это поле обязательно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public List<Exception>? error { get; set; }
        public string? Message { get; set; }
        public string? ConfirmCode { get; set; }
    }
}

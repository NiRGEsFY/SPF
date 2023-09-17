using System.ComponentModel.DataAnnotations;

namespace SPF.Models
{
    public class UserCreate
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Должность пользователя")]
        public string PostUser { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}

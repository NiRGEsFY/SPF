using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SPF.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Должность")]
        public string PostUser { get; set; }
    }
}

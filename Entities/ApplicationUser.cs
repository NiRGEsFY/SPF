using Microsoft.AspNetCore.Identity;

namespace SPF.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostUser { get; set; }

    }
}

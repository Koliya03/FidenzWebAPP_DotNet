using Microsoft.AspNetCore.Identity;

namespace FidenzApp.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Department { get; set; } = string.Empty;

    }
}

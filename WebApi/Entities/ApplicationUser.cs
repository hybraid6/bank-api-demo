using Microsoft.AspNetCore.Identity;

namespace WebApi.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}

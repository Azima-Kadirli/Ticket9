using Microsoft.AspNetCore.Identity;

namespace Ticket9.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}

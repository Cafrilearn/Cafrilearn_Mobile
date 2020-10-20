using Microsoft.AspNetCore.Identity;

namespace AfriLearnBackend.Models
{
    public class AppUser : IdentityUser
    {

        public  string  Role { get; set; }
        public  Setting  Setting { get; set; }
    }
}

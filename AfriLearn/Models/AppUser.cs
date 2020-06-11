using Microsoft.AspNetCore.Identity;

namespace AfriLearnMobile.Models
{
    class AppUser : IdentityUser
    {
        public bool TermsAndConditionsChecked { get; set; }
        public string StudyLevel { get; set; }
        public bool IsSignedIn { get; set; }
        public string School { get; set; }
        public string ProfilePhotoPath { get; set; }
        public  string  Institution { get; set; }
    }
}

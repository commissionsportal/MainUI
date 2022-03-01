using Microsoft.AspNetCore.Identity;

namespace MainUI.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string ApiToken { get; set; }
        public string Initials
        {
            get
            {
                return this.UserName.Substring(0, 2);
            }
        }
    }
}

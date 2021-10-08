using Microsoft.AspNetCore.Identity;

namespace MainUI.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string ApiToken { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MainUI.Authentication
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            Microsoft.Extensions.Options.IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            if (user.ApiToken != null)
            {
                var principal = await base.CreateAsync(user);
                var identity = (ClaimsIdentity)principal.Identity;

                var claims = new List<Claim>();

                claims.Add(new Claim("Token", user.ApiToken));

                identity.AddClaims(claims);
                return principal;
            }

            return null;
        }
    }

    public static class ClaimsExtensionMethods
    {
        public static string GetToken(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(x => x.Type == "Token").FirstOrDefault()?.Value ?? string.Empty;
        }
    }
}

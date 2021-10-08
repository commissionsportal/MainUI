using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MainUI.Authentication
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole> roleStore,
            IEnumerable<IRoleValidator<IdentityRole>> roleValidator,
            ILookupNormalizer lookupNormalizer,
            IdentityErrorDescriber identityErrorDescriber,
            ILogger<RoleManager<IdentityRole>> logger) : base(roleStore, roleValidator, lookupNormalizer, identityErrorDescriber, logger)
        {

        }
    }
}

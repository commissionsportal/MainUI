using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.Authentication
{
    public static class AuthenticationExtensionMethods
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationUser>, RoleStore>();
            services.AddTransient<IRoleStore<IdentityRole>, RoleStore>();
            services.AddTransient<IPasswordHasher<ApplicationUser>, PasswordHasher>();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true);

            var sp = services.BuildServiceProvider();
            var apiToken = sp.GetService<IConfiguration>().GetValue<string>("ApiToken");

            if (!string.IsNullOrWhiteSpace(apiToken))
            {
                services.AddSingleton<IAuthTokenProvider>(new ApiKeyTokenProvider(apiToken));
            }
            else
            {
                services.AddTransient<IAuthTokenProvider, LoginTokenProvider>();
            }

            services.AddScoped<RoleManager<IdentityRole>, ApplicationRoleManager>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AdditionalUserClaimsPrincipalFactory>();
        }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.Authentication
{
    public static class AuthenticationExtensionMethods
    {
        public static void AddIdentity(this IServiceCollection services, string apiToken = "")
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationUser>, RoleStore>();
            services.AddTransient<IRoleStore<IdentityRole>, RoleStore>();
            services.AddTransient<IPasswordHasher<ApplicationUser>, PasswordHasher>();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true);

            var sp = services.BuildServiceProvider();
            var configuration = sp.GetService<IConfiguration>();
            var authUrl = configuration.GetValue<string>("AuthUrl", "https://auth.commissionsportal.com");

            if (!string.IsNullOrWhiteSpace(authUrl))
            {
                services.AddTransient<IAuthTokenProvider, LoginTokenProvider>();
            }
            else if(!string.IsNullOrWhiteSpace(apiToken))
            {
                services.AddSingleton<IAuthTokenProvider>(new ApiKeyTokenProvider(apiToken));
            }

            services.AddScoped<RoleManager<IdentityRole>, ApplicationRoleManager>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AdditionalUserClaimsPrincipalFactory>();
        }
    }
}

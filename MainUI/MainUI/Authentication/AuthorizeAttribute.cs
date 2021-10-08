using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MainUI.Authentication
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IAuthTokenProvider _authTokenProvider = (IAuthTokenProvider)context.HttpContext.RequestServices.GetService(typeof(IAuthTokenProvider));

            if (!_authTokenProvider.HasToken())
            {
                context.Result = new RedirectResult("/Identity/Account/Login?ReturnUrl=%2F");
            }
        }
    }
}

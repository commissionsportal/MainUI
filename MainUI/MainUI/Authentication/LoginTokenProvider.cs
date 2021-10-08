using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MainUI.Authentication
{
    public class LoginTokenProvider : IAuthTokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginTokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetToken()
        {
            return _contextAccessor.HttpContext.User.GetToken();
        }

        public bool HasToken()
        {
            return !string.IsNullOrWhiteSpace(GetToken());
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return !string.IsNullOrWhiteSpace(principal.GetToken());
        }
    }
}

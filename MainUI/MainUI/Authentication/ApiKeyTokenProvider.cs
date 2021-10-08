using System.Security.Claims;

namespace MainUI.Authentication
{
    public class ApiKeyTokenProvider : IAuthTokenProvider
    {
        private readonly string _apiToken;

        public ApiKeyTokenProvider(string apiToken)
        {
            _apiToken = apiToken;
        }

        public string GetToken()
        {
            return _apiToken;
        }

        public bool HasToken()
        {
            return true;
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return false;
        }
    }
}

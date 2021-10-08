using System.Security.Claims;

namespace MainUI.Authentication
{
    public interface IAuthTokenProvider
    {
        string GetToken();
        bool HasToken();
        bool IsSignedIn(ClaimsPrincipal principal);
    }
}

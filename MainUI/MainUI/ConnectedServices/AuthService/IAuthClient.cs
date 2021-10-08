using System.Threading.Tasks;

namespace MainUI.ConnectedServices.AuthService
{
    public interface IAuthClient
    {
        Task<T> GetValue<T>(string url);
    }
}

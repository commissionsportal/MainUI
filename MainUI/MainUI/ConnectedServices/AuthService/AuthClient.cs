using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.AuthService
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string _baseUrl;

        public AuthClient(IConfiguration configuration)
        {
            _baseUrl = configuration.GetValue<string>("AuthUrl", "https://auth.commissionsportal.com");
        }

        public async Task<T> GetValue<T>(string url)
        {
            var result = await client.GetAsync(_baseUrl + url);
            return await ProcessResult<T>(result);
        }

        private async Task<T> ProcessResult<T>(HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception();
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK ||
                responseMessage.StatusCode == System.Net.HttpStatusCode.Created ||
                responseMessage.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default(T);
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception(content);
            }

            throw new Exception(content);
        }
    }
}

using MainUI.Authentication;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices
{
    public interface IQueryClient
    {
        Task<T> PostQuery<T>(QLQuery query);
    }

    public class QueryClient : IQueryClient
    {
        private readonly IAuthTokenProvider _authTokenProvider;
        private readonly HttpClient client = new HttpClient();

        private readonly string _baseUrl = "https://api.commissionsportal.com/graphql/";

        public QueryClient(IAuthTokenProvider authTokenProvider)
        {
            _authTokenProvider = authTokenProvider;
        }

        public async Task<T> PostQuery<T>(QLQuery query)
        {
            var authToken = _authTokenProvider.GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authToken);

            var result = await client.PostAsJsonAsync(_baseUrl, query);
            var json = await result.Content.ReadAsStringAsync();

            var queryResult = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryResult<T>>(json);
            return queryResult.Data;
        }
    }

    public static class QueryClientExtensionMethods
    {
        public static Task<T> PostQuery<T>(this IQueryClient client, string query)
        {
            return client.PostQuery<T>(new QLQuery
            {
                Query = query
            });
        }
    }
}

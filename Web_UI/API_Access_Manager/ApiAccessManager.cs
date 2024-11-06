using System.Net.Http.Headers;

namespace Web_UI.API_SERVİCE
{
    public class ApiAccessManager
    {
        public readonly HttpClient _client;

        public ApiAccessManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _client = httpClient;
            var token = httpContextAccessor.HttpContext.Session.GetString("UserToken");
            if (token != null)
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

    }
}

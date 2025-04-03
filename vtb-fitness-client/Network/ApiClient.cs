using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using vtb_fitness_client.Dto;
using vtb_fitness_client.Model;

namespace vtb_fitness_client.Network
{
    public static class ApiClient
    {
        private static readonly string _apiPath = "http://localhost:5047/api/";
        private static HttpClient _httpClient = new HttpClient();

        private static async Task<T?> SendRequest<T>(string url, HttpMethod method, object? body = null)
        {
            using var request = new HttpRequestMessage(method, _apiPath + url);

            if (body != null)
            {
                var jsonContent = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);

            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }

            return default;
        }

        public static class _User
        {
            public static async Task<User?> SignIn(string login, string password)
            {
                var dto = new
                {
                    Login = login,
                    Password = password
                };
                return await SendRequest<User>("user/sign_in", HttpMethod.Post, dto);
            }

            public static async Task<User?> SignUp(UserSignUpDto dto)
            {
                return await SendRequest<User>("user/sign_up", HttpMethod.Post, dto);
            }
        }

        public static class _Tariff
        {
            public static async Task<List<Tariff>> Get() => await SendRequest<List<Tariff>>("tariff", HttpMethod.Get);

            public static async Task<Tariff?> Create(TariffCreateDto dto) => await SendRequest<Tariff>("tariff", HttpMethod.Post, dto);
        }
    }
}

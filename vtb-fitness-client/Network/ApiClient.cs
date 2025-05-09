using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows.Navigation;
using vtb_fitness_client.Dto;
using vtb_fitness_client.Model;
using vtb_fitness_client.Utility;

namespace vtb_fitness_client.Network
{
    public static class ApiClient
    {
        private static readonly string _apiPath = "http://127.0.0.1:5047/api/";
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

            public static async Task<List<User>> Get() => await SendRequest<List<User>>("user", HttpMethod.Get);

            public static async Task<User> GetById(int id) => await SendRequest<User>($"user/{id}", HttpMethod.Get);

            public static async Task<UserTariff?> GetCurrentTariff(int id)
            {
                return await SendRequest<UserTariff>($"user/{id}/current_tariff", HttpMethod.Get);
            }

            public static async Task<List<User>> Search(string q, UserSearchType type)
            {
                return type switch
                {
                    UserSearchType.Users => await SendRequest<List<User>>("user/search/users", HttpMethod.Get),
                    UserSearchType.Mods => await SendRequest<List<User>>("user/search/mods", HttpMethod.Get),
                    UserSearchType.Trainers => await SendRequest<List<User>>("user/search/trainers", HttpMethod.Get)
                };
            }

            public static async Task<List<Tracker>> GetTracker(int id) => await SendRequest<List<Tracker>>($"user/{id}/tracker", HttpMethod.Get);

            public static async Task<int?> GetRemainingTrainerWorkouts(int userId) => await SendRequest<int?>($"user/{userId}/remaining_trainer_workouts", HttpMethod.Get);
        }

        public static class _Tariff
        {
            public static async Task<List<Tariff>> Get() => await SendRequest<List<Tariff>>("tariff", HttpMethod.Get);

            public static async Task<Tariff?> Create(TariffCreateDto dto) => await SendRequest<Tariff>("tariff", HttpMethod.Post, dto);
            public static async Task<UserTariff> Purchase(TariffPurchaseDto dto) => await SendRequest<UserTariff>("tariff/purchase", HttpMethod.Post, dto);
        }

        public static class _Role
        {
            public static async Task<List<Role>> Get() => await SendRequest<List<Role>>("role", HttpMethod.Get);
            public static async Task<Role> GetById(int? id) => await SendRequest<Role>($"role/{id}", HttpMethod.Get);
        }

        public static class _Exercise
        {
            public static async Task<List<Exercise>> Get() => await SendRequest<List<Exercise>>("exercise", HttpMethod.Get);
            public static async Task<List<Exercise>> GetCardio() => await SendRequest<List<Exercise>>("exercise/cardio", HttpMethod.Get);
            public static async Task<List<Exercise>> GetStrength() => await SendRequest<List<Exercise>>("exercise/strength", HttpMethod.Get);
            public static async Task<List<Exercise>> GetWeight() => await SendRequest<List<Exercise>>("exercise/weight", HttpMethod.Get);
            public static async Task<Exercise> GetByName(string name) => await SendRequest<Exercise>($"exercise/name?name={name}", HttpMethod.Get);

            
        }

        public static class _Tracker
        {
            public static async Task<Tracker> Create(TrackerCreateDto dto) => await SendRequest<Tracker>("tracker", HttpMethod.Post, dto);
        }


    }
}

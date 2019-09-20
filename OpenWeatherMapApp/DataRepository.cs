using Newtonsoft.Json;
using OpenWeatherMapApp.Models;
using System.Net.Http;
using System.Threading.Tasks;


namespace OpenWeatherMapApp
{
    public class DataRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public DataRepository(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public WeatherModel GetWeahter(string city) => GetWeahterAsync(city).GetAwaiter().GetResult();

        private async Task<string> GetWeahterJsonAsync(string city)
        {
            var responce = await _httpClient.GetAsync(string.Format("http://api.openweathermap.org/data/2.5/weather?appid={0}&q={1}&units=metric", _apiKey, city));
            responce.EnsureSuccessStatusCode();

            var json = await responce.Content.ReadAsStringAsync();
            return json;
        }

        private async Task<WeatherModel> GetWeahterAsync(string city)
        {
            var json = await GetWeahterJsonAsync(city);
            var data = JsonConvert.DeserializeObject<WeatherModel>(json);

            return data;
        }
    }
}


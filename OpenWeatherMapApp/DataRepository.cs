using Newtonsoft.Json;
using OpenWeatherMapApp.Models;
using System.Net.Http;
using System.Threading.Tasks;


namespace OpenWeatherMapApp
{
    public class DataRepository
    {
        private readonly HttpClient _httpClient;

        public DataRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public WeatherModel GetWeahter(string city) => GetWeahterAsync(city).GetAwaiter().GetResult();

        private async Task<string> GetWeahterJsonAsync(string city)
        {
            var responce = await _httpClient.GetAsync(string.Format("http://api.openweathermap.org/data/2.5/weather?appid=5f9d8a8b65c4a8651d769c34038a55f3&q={0}&lang=ru&units=metric", city));
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


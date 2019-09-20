using Newtonsoft.Json;

namespace OpenWeatherMapApp.Models
{
    public partial class Clouds
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }
}

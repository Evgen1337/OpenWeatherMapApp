using Newtonsoft.Json;

namespace OpenWeatherMapApp.Models
{
    public partial class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public long Deg { get; set; }
    }
}

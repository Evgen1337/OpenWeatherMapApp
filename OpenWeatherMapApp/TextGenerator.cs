using OpenWeatherMapApp.Models;
using System;
using System.Text;


namespace OpenWeatherMapApp
{
    public static class TextGenerator
    {
        public static string GenerateWeahterText(WeatherModel weahter)
        {
            var sb = new StringBuilder();

            var sunsetDateTime = ConvertFromUnixTimestamp(weahter.Sys.Sunset);
            var sunriseDateTime = ConvertFromUnixTimestamp(weahter.Sys.Sunrise);

            sb.AppendLine(string.Format("{0} - {1} celsius", nameof(weahter.Main.Temp), weahter.Main.Temp));
            sb.AppendLine(string.Format("{0} - {1}", nameof(weahter.Main.Humidity), weahter.Main.Humidity));
            sb.AppendLine(string.Format("{0} - {1}", nameof(weahter.Sys.Sunset), sunsetDateTime));
            sb.AppendLine(string.Format("{0} - {1}", nameof(weahter.Sys.Sunrise), sunriseDateTime));

            var weahterDataToInsert = sb.ToString();
            sb.Clear();

            return weahterDataToInsert;
        }

        static TimeSpan ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp).ToLocalTime().TimeOfDay;
        }

        public static string GenerateReportText(WeatherModel weahter)
        {
            var sb = new StringBuilder();

            var weahterText = GenerateWeahterText(weahter);

            sb.AppendLine(string.Format("Report generate date - {0}", DateTime.Now));
            sb.AppendLine(weahterText);
            sb.AppendLine("____________________");

            var report = sb.ToString();

            return report;
        }
    }
}


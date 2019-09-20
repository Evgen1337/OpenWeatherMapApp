using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;


namespace OpenWeatherMapApp
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static readonly string ApiKey = ConfigurationManager.AppSettings[nameof(ApiKey)];


        static void Main(string[] args)
        {
            try
            {
                Logger.Info("sdf");
                Run();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex);
                Console.WriteLine(ex.Message);
            }

        }

        private static void Run()
        {
            var httpClient = new HttpClient();
            var dataRepository = new DataRepository(httpClient, ApiKey);

            while (true)
            {
                Console.WriteLine("Please enter city in eng lang");
                var city = Console.ReadLine();

                if (city == string.Empty || string.IsNullOrWhiteSpace(city))
                {
                    Console.WriteLine("Please write city");
                    continue;
                }

                var weahter = dataRepository.GetWeahter(city);
                var dataToInsert = TextGenerator.GenerateWeahterText(weahter);
                Console.WriteLine(dataToInsert);

                Console.WriteLine("Save info at txt file?(yes: key \"y\" / no: any key)");
                var saveResponce = Console.ReadLine();
                if (saveResponce == "y")
                {
                    Directory.CreateDirectory("data");

                    var reportText = TextGenerator.GenerateReportText(weahter);
                    File.AppendAllLines("data/reports.txt", new[] { reportText });

                    Console.WriteLine("Report save sucsses");
                }

                Console.WriteLine("Check new wehter?(yes: key \"y\" / no: any key)");
                var continuieResponce = Console.ReadLine();
                if (continuieResponce != "y")
                    break;
            }
        }
    }
}


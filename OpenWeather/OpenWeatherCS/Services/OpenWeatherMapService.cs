using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenWeatherCS.Models;
using System.Net.Http;
using System.IO;
using System.Xml.Linq;
using OpenWeatherCS.Utils;
using System.Net;
using System.Diagnostics;

namespace OpenWeatherCS.Services
{
    public class OpenWeatherMapService : IWeatherService
    {
        private const string APP_ID = "dd2d3896e419bd6317cbc1f12978192d";
        private const int MAX_FORECAST_DAYS = 5;
        private HttpClient client;

        public OpenWeatherMapService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecastAsync(string location, int days)
        {
            if (location == null) throw new ArgumentNullException("Location can't be null.");
            if (location == string.Empty) throw new ArgumentException("Location can't be an empty string.");
            if (days <= 0) throw new ArgumentOutOfRangeException("Days should be greater than zero.");
            if (days > MAX_FORECAST_DAYS) throw new ArgumentOutOfRangeException($"Days can't be greater than {MAX_FORECAST_DAYS}");

            var query = $"forecast?q={location}&type=accurate&mode=xml&units=metric&APPID={APP_ID}";
            var response = await client.GetAsync(query);
            string fileName = location + ".xml";

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedApiAccessException("Invalid API key.");
                case HttpStatusCode.NotFound:
                    throw new LocationNotFoundException("Location not found.");
                case HttpStatusCode.OK:
                var s = await response.Content.ReadAsStringAsync();
                var x = XElement.Load(new StringReader(s));
                File.WriteAllText(fileName, s);

                var data = x.Descendants("time").Select(w => new WeatherForecast
                {
                    Description = w.Element("symbol").Attribute("name").Value,
                    ID = int.Parse(w.Element("symbol").Attribute("number").Value),
                    IconID = w.Element("symbol").Attribute("var").Value,
                    Date = DateTime.Parse(w.Attribute("from").Value),
                    WindType = w.Element("windSpeed").Attribute("name").Value,
                    WindSpeed = double.Parse(w.Element("windSpeed").Attribute("mps").Value),
                    WindDirection = w.Element("windDirection").Attribute("code").Value,
                    DayTemperature = double.Parse(w.Element("temperature").Attribute("value").Value),
                    NightTemperature = double.Parse(w.Element("temperature").Attribute("value").Value),
                    MaxTemperature = double.Parse(w.Element("temperature").Attribute("max").Value),
                    MinTemperature = double.Parse(w.Element("temperature").Attribute("min").Value),
                    Pressure = double.Parse(w.Element("pressure").Attribute("value").Value),
                    Humidity = double.Parse(w.Element("humidity").Attribute("value").Value),
                    City = location
                });

                return data;
            default:
                if (File.Exists(fileName))
                {
                    XElement data1 = XElement.Load(fileName);
                    var data2 = data1.Descendants("time").Select(w => new WeatherForecast
                    {
                        Description = w.Element("symbol").Attribute("name").Value,
                        ID = int.Parse(w.Element("symbol").Attribute("number").Value),
                        IconID = w.Element("symbol").Attribute("var").Value,
                        Date = DateTime.Parse(w.Attribute("from").Value),
                        WindType = w.Element("windSpeed").Attribute("name").Value,
                        WindSpeed = double.Parse(w.Element("windSpeed").Attribute("mps").Value),
                        WindDirection = w.Element("windDirection").Attribute("code").Value,
                        DayTemperature = double.Parse(w.Element("temperature").Attribute("value").Value),
                        NightTemperature = double.Parse(w.Element("temperature").Attribute("value").Value),
                        MaxTemperature = double.Parse(w.Element("temperature").Attribute("max").Value),
                        MinTemperature = double.Parse(w.Element("temperature").Attribute("min").Value),
                        Pressure = double.Parse(w.Element("pressure").Attribute("value").Value),
                        Humidity = double.Parse(w.Element("humidity").Attribute("value").Value),
                        City = location
                    });
                    return data2;
                }
                else
                {
                    throw new NotImplementedException(response.StatusCode.ToString());
                }
            }           
        }
    }
}
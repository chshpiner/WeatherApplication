using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using OpenWeatherCS.Utils;
using OpenWeatherCS.Commands;
using OpenWeatherCS.Services;
using OpenWeatherCS.Models;
using System.Net.Http;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using System.Data.Entity;
using System.IO;
using System.Xml.Linq;

namespace OpenWeatherCS.ViewModels
{
    public class WeatherContext25 : DbContext
    {
        public DbSet<WeatherForecast> forc { get; set; }

    }

    public class WeatherViewModel : ViewModelBase
    {
        private IWeatherService weatherService;
        private IDialogService dialogService;

        public WeatherViewModel(IWeatherService ws, IDialogService ds)
        {
            weatherService = ws;
            dialogService = ds;
        }

        private List<WeatherForecast> _forecast;
        public List<WeatherForecast> Forecast
        {
            get { return _forecast; }
            set
            {
                _forecast = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection _series = new SeriesCollection(){
        new LineSeries
            {
                Values = new ChartValues<double> { 31, 29, 30, 26 }
             },
            new ColumnSeries
            {
                Values = new ChartValues<decimal> { 31, 29, 30, 26 }
            }
        };
        public SeriesCollection Series {
            get { return _series; }
            set
            {
                _series = value;
                OnPropertyChanged();
            }
        }

        private WeatherForecast _currentWeather = new WeatherForecast();
        public WeatherForecast CurrentWeather
        {
            get { return _currentWeather; }
            set
            {
                _currentWeather = value;
                OnPropertyChanged();
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        private ICommand _getWeatherCommand;
        public ICommand GetWeatherCommand
        {
            get
            {
                if (_getWeatherCommand == null) _getWeatherCommand = 
                        new RelayCommandAsync(() => GetWeather(), (o) => CanGetWeather());               
                return _getWeatherCommand;
            }
        }

        public async Task GetWeather()
        {
            try
            {
                var weather = await weatherService.GetForecastAsync(Location, 3);
                var weatherDaily = GetDailyWeather(weather);
                CurrentWeather = weatherDaily.First();
                Forecast = weatherDaily.Skip(1).Take(4).ToList();
                loadChart(Forecast);
            }
            catch (HttpRequestException ex) {
                string fileName = Location + ".xml";
                if (File.Exists(fileName))
                {
                    XElement data1 = XElement.Load(fileName);
                    var weather = data1.Descendants("time").Select(w => new WeatherForecast
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
                        City = Location
                    });
                    var weatherDaily = GetDailyWeather(weather);
                    CurrentWeather = weatherDaily.First();
                    Forecast = weatherDaily.Skip(1).Take(4).ToList();
                    loadChart(Forecast);
                }
                else
                {
                    dialogService.ShowNotification("Ensure you're connected to the internet!", "Open Weather");
                }
            }
            catch (LocationNotFoundException ex)
            {
                dialogService.ShowNotification("Location not found!", "Open Weather");
            }
            
        }

        public Boolean CanGetWeather()
        {
            return Location != string.Empty;
        }

        public List<WeatherForecast> GetDailyWeather(IEnumerable<WeatherForecast> weather)
        {
            List<WeatherForecast> Forecast = new List<WeatherForecast>();
            int j = 0;
            int k = 0;
            int p = weather.Count();
            while (j < 3 && k < weather.Count()) 
            {
                DateTime nullptr = default(DateTime);
                DateTime date = nullptr;
                WeatherForecast nullptr1 = default(WeatherForecast);
                WeatherForecast cur = nullptr1;
                double mint = 0;
                double maxt = 100000;
                for (int i = k; i < weather.Count(); i++)
                {
                    var w = weather.ElementAt<WeatherForecast>(i);
                    DateTime newdate = w.Date;
                    k = i + 1;
                    if (date == nullptr)
                    {
                        date = newdate;
                        cur = w;
                        mint = w.MinTemperature;
                        maxt = w.MaxTemperature;
                    }
                    else if (date.Day.Equals(newdate.Day))
                    {
                        mint = (mint < w.MinTemperature) ? mint : w.MinTemperature;
                        maxt = (maxt > w.MaxTemperature) ? maxt : w.MaxTemperature;
                        if (newdate.Hour == 12)
                        {
                            cur = w;
                        }
                    }
                    else break;
                }
                cur.MaxTemperature = maxt;
                cur.MinTemperature = mint;
                Forecast.Add(cur);
            }
            return Forecast;
        }

        private void loadChart(List<WeatherForecast> forecast)
        {
            double tmp_1 = Math.Round(forecast.First().MaxTemperature, 1);
            string day_1_ = forecast.ElementAt<WeatherForecast>(0).Date.DayOfWeek.ToString();
            double tmp_2 = Math.Round(forecast.ElementAt<WeatherForecast>(1).MaxTemperature, 1);
            string day_2_ = forecast.ElementAt<WeatherForecast>(1).Date.DayOfWeek.ToString();
            double tmp_3 = Math.Round(forecast.ElementAt<WeatherForecast>(2).MaxTemperature, 1);
            string day_3_ = forecast.ElementAt<WeatherForecast>(2).Date.DayOfWeek.ToString();
            double tmp_4 = Math.Round(forecast.ElementAt<WeatherForecast>(3).MaxTemperature, 1);
            string day_4_ = forecast.ElementAt<WeatherForecast>(3).Date.DayOfWeek.ToString();

            double tm_1 = Math.Round(forecast.First().MinTemperature, 1);
            double tm_2 = Math.Round(forecast.ElementAt<WeatherForecast>(1).MinTemperature, 1);
            double tm_3 = Math.Round(forecast.ElementAt<WeatherForecast>(2).MinTemperature, 1);
            double tm_4 = Math.Round(forecast.ElementAt<WeatherForecast>(3).MinTemperature, 1);

            SeriesCollection series = new SeriesCollection {};
            string[] Labels = new[] { day_1_, day_2_, day_3_, day_4_ };
            Func<double, string> YFormatter = value => value.ToString() + "°С";

            //modifying the series collection will animate and update the chart
            series.Add(new LineSeries
            {
                Title = "Max Temperature",
                Values = new ChartValues<double> { tmp_1, tmp_2, tmp_3, tmp_4, 30 },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 15,
                PointForeground = Brushes.Red
            });

            series.Add(new LineSeries
            {
                Title = "Min Temperature",
                Values = new ChartValues<double> { tm_1, tm_2, tm_3, tm_4, 25 },
                LineSmoothness = 0.8, //0: straight lines, 1: really smooth lines
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 15,
                PointForeground = Brushes.Gold
            });

            Series = series;

        }
    }
}

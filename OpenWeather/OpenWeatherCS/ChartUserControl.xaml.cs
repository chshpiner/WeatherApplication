using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using OpenWeatherCS.Models;

namespace OpenWeatherCS
{
    /// <summary>
    /// Interaction logic for ChartUserControl.xaml
    /// </summary>
    public partial class ChartUserControl : UserControl
    {
        public OpenWeatherCS.Utils.ViewModelLocator vm;

        public ChartUserControl()
        {
            List<WeatherForecast> forecast = vm.WeatherVM.Forecast;

            double tmp_1 = Math.Round(forecast.First().MaxTemperature, 1);
            string day_1_ = forecast.ElementAt<WeatherForecast>(0).Date.DayOfWeek.ToString();
            double tmp_2 = Math.Round(forecast.ElementAt<WeatherForecast>(1).MaxTemperature, 1);
            string day_2_ = forecast.ElementAt<WeatherForecast>(1).Date.DayOfWeek.ToString();
            double tmp_3 = Math.Round(forecast.ElementAt<WeatherForecast>(2).MaxTemperature, 1);
            string day_3_ = forecast.ElementAt<WeatherForecast>(2).Date.DayOfWeek.ToString();
            double tmp_4 = Math.Round(forecast.ElementAt<WeatherForecast>(3).MaxTemperature, 1);
            string day_4_ = forecast.ElementAt<WeatherForecast>(3).Date.DayOfWeek.ToString();

            SeriesCollection = new SeriesCollection
            {


            };
            Labels = new[] { day_1_, day_2_, day_3_, day_4_};
            YFormatter = value => value.ToString() + "°С";

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Title = "",
                Values = new ChartValues<double> { tmp_1, tmp_2, tmp_3, tmp_4},
                LineSmoothness = 0.6, //0: straight lines, 1: really smooth lines
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 15,
                PointForeground = Brushes.Red
            });

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }


    }
}

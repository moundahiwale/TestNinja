using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using TestNinja.Config;

namespace TestNinja.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetForecast();
    }

    public class WeatherService : IWeatherService
    {
        private readonly WeatherForecastSettings _forecastSettings;

        public WeatherService(IOptions<WeatherForecastSettings> options)
        {
            _forecastSettings = options.Value;
        }

        public IEnumerable<WeatherForecast> GetForecast()
        {
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = _forecastSettings.Summaries[rng.Next(_forecastSettings.Summaries.Length)]
            })
            .ToArray();
        }
    }
}
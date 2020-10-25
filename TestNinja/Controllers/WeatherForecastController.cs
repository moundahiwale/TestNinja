using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestNinja.Services;

namespace TestNinja.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Lazy<IWeatherService> _weatherService;

        public WeatherForecastController(Lazy<IWeatherService> weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherService.Value.GetForecast();
        }
    }
}

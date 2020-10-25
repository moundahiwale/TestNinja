using System;
using System.Linq;
using Moq;
using TestNinja.Controllers;
using TestNinja.Services;
using Xunit;

namespace TestNinja.UnitTests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private WeatherForecast[] _weatherForecasts =
        {
            new WeatherForecast{Date = DateTime.Now, TemperatureC = -14, Summary = "Mild"},
            new WeatherForecast{Date = DateTime.Now, TemperatureC = -1, Summary = "Cool"}
        };
        private readonly WeatherForecastController _controller;
        private readonly Mock<IWeatherService> _weatherService;

        public WeatherForecastControllerTests()
        {
            _weatherService = new Mock<IWeatherService>();
            _weatherService.Setup(ws => ws.GetForecast()).Returns(_weatherForecasts);

            _controller = new WeatherForecastController(_weatherService.Object);
        }

        [Fact]
        public void WeatherForecastController_WhenGetIsCalled_ShouldGetForecast()
        {
            var result = _controller.Get();

            Assert.Equal(result, _weatherForecasts);
        }
    }
}
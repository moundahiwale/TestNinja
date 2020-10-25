using System.Linq;
using TestNinja.Controllers;
using Xunit;

namespace TestNinja.UnitTests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private const int _forecastCount = 5;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            _controller = new WeatherForecastController();
        }

        [Fact]
        public void WeatherForecastController_WhenGetIsCalled_ShouldGetForecast()
        {
            var result = _controller.Get();

            // May use any of the following
            Assert.NotEmpty(result);
            Assert.Equal(result.Count(), _forecastCount);
            Assert.IsType<WeatherForecast[]>(result);
        }
    }
}
using System.Linq;
using Microsoft.Extensions.Options;
using Moq;
using TestNinja.Config;
using TestNinja.Services;
using Xunit;

namespace TestNinja.UnitTests.Services
{
    public class WeatherServiceTests
    {
        private const int _forecastCount = 5;
        private readonly Mock<IOptions<WeatherForecastSettings>> _options;
        private string[] _summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        public WeatherServiceTests()
        {
            _options = new Mock<IOptions<WeatherForecastSettings>>();
            _options.Setup(o => o.Value).Returns(new WeatherForecastSettings { Summaries = _summaries });
        }
        [Fact]
        public void GetForecast_ShouldReturn_WeatherForecast()
        {
            var _service = new WeatherService(_options.Object);

            var result = _service.GetForecast();

            Assert.Equal(result.Count(), _forecastCount);
        }
    }
}
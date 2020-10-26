using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace TestNinja.IntegrationTests.Controllers
{
    public class WeatherForecastControllerIntegrationTests
    {
        [Fact]
        public async Task GetWeatherForecast()
        {
            using var client = new TestClientProvider().Client;
            var response = await client.GetAsync("/WeatherForecast");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
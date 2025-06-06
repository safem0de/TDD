using TDD.Api.Controllers.v1;

namespace TDD.Api.Tests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void Get_ReturnsFiveForecasts()
        {
            // Arrange
            var controller = new WeatherForecastController();

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());

            // Optional: ตรวจสอบค่าที่ได้
            foreach (var forecast in result)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.False(string.IsNullOrEmpty(forecast.Summary));
            }
        }

        [Fact]
        public void TemperatureF_IsCalculatedCorrectly()
        {
            // Arrange
            var forecast = new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now),
                0,  // 0°C
                "Chilly");

            // Act
            var temperatureF = forecast.TemperatureF;

            // Assert: 0°C = 32°F
            Assert.Equal(32, temperatureF);
        }
    }
}
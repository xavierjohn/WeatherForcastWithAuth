using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Weather;
using WeatherTests.TestAuthentication;
using Xunit;

namespace WeatherTests
{
    public class WeatherForcastTests : IClassFixture<AuthWebApplicationFactory<Startup>>
    {
        private readonly AuthWebApplicationFactory<Startup> _factory;
        public WeatherForcastTests(AuthWebApplicationFactory<Startup> factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task Authorized_user_with_access_as_user_scope_can_get_the_forcast()
        {
            // Arrange
            var client = _factory
                .WithTestClaim(new List<Claim>() {
                       new Claim("scp", "access_as_user")
                })
                .CreateClient();

            // Act
            var response = await client.GetAsync("WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var weather = await response.Content.ReadAsStringAsync();
            Assert.Contains("temperatureF", weather);
        }

        [Fact]
        public async Task Authorized_user_without_access_as_user_scope_cannot_get_the_forcast()
        {
            // Arrange
            var client = _factory
                .WithTestClaim(new List<Claim>() {
                       new Claim("scp", "foobar")
                })
                .CreateClient();

            var execptionWasThrown = false;

            // Act
            try
            {
                var response = await client.GetAsync("WeatherForecast");

                // Assert
                Assert.False(true, "Should have thrown excepton.");
            }
            catch(Exception )
            {
                execptionWasThrown = true;
            }

            Assert.True(execptionWasThrown);
        }
    }

}

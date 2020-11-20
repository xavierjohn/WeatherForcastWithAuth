using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Security.Claims;
using Weather;
using Xunit;

namespace WeatherTests.TestAuthentication
{
    public class TestWebApplicationFactory: WebApplicationFactory<Startup>
    {
        public WebApplicationFactory<Startup> WithTestSchemeAuth(IReadOnlyList<Claim> claims)
        {
            return WithWebHostBuilder(builder => builder.ConfigureTestServices(
                   services =>
                   {
                       services.AddAuthentication(TestAuthenticationOptions.DefaultScheme)
                       .AddTestAuthentication(options =>
                       {
                           options.Claims = claims;
                       });
                   }
               ));
        }

        [CollectionDefinition("Controller collection")]
        public class ControllerCollection : ICollectionFixture<TestWebApplicationFactory>
        {
            // This class has no code, and is never created. Its purpose is simply
            // to be the place to apply [CollectionDefinition] and all the
            // ICollectionFixture<> interfaces.
        }

    }
}

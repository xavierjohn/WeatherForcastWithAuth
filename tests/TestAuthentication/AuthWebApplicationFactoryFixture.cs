using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Security.Claims;

namespace WeatherTests.TestAuthentication
{
    public class AuthWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        public WebApplicationFactory<TEntryPoint> WithTestClaim(IReadOnlyList<Claim> claims)
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


    }
}

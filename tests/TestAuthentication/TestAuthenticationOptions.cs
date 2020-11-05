using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace WeatherTests.TestAuthentication
{
    internal class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "Test Authentication";
        public IReadOnlyList<Claim> Claims { get; set; }
    }
}

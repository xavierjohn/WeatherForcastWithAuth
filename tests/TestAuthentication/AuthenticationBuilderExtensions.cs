using Microsoft.AspNetCore.Authentication;
using System;

namespace WeatherTests.TestAuthentication
{
    public static class AuthenticationBuilderExtensions
    {
        internal static AuthenticationBuilder AddTestAuthentication(this AuthenticationBuilder builder, Action<TestAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(TestAuthenticationOptions.DefaultScheme, configureOptions);
        }
    }
}

namespace Weather.Tests.TestAuthentication;
using Microsoft.AspNetCore.Authentication;

public static class AuthenticationBuilderExtensions
{
    internal static AuthenticationBuilder AddTestAuthentication(this AuthenticationBuilder builder, Action<TestAuthenticationSchemeOptions> configureOptions)
        => builder.AddScheme<TestAuthenticationSchemeOptions, TestAuthenticationHandler>(TestAuthenticationSchemeOptions.DefaultScheme, configureOptions);
}

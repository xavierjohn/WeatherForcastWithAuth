namespace Weather.Tests.TestAuthentication;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

internal class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public TestAuthenticationSchemeOptions()
    {
        TimeProvider = TimeProvider.System;
    }

    public const string DefaultScheme = "Test Authentication";
    public IReadOnlyList<Claim> Claims { get; set; } = [];
}

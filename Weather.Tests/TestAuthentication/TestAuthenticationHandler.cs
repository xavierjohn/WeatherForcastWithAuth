namespace Weather.Tests.TestAuthentication;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

internal class TestAuthenticationHandler(IOptionsMonitor<TestAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<TestAuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Create authenticated user
        var claimsIdentity = new ClaimsIdentity(Options.Claims, "Test Auth claims identity");
        var identities = new List<ClaimsIdentity> { claimsIdentity };
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), TestAuthenticationSchemeOptions.DefaultScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

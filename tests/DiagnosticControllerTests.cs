using FluentAssertions;
using Serilog;
using Serilog.Sinks.InMemory;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Serilog.Sinks.InMemory.Assertions;
using WeatherTests.TestAuthentication;
using System.Collections.Generic;
using System.IO;
using Serilog.Formatting.Compact;
using System.Threading;

namespace WeatherTests
{
    [Collection("Controller collection")]
    public class DiagnosticControllerTests
    {
        private readonly TestWebApplicationFactory _factory;

        public DiagnosticControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Server_exceptions_are_handled()
        {
            // Arrange
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .WriteTo.InMemory()
                .CreateLogger();

            var client = _factory
                            .WithTestSchemeAuth(new List<Claim>() {
                                new Claim("scp", "foobar")
                            })
                            .CreateClient();
            var expectedCorrelationId = "SomeBar";
            var request = new HttpRequestMessage(HttpMethod.Options, "diagnostic/throw");
            request.Headers.Add("CorrelationId", expectedCorrelationId);

            // Act
            var response = await client.SendAsync(request);

            // Assert

            response.IsSuccessStatusCode.Should().BeFalse();
            var errorResponse = await response.Content.ReadAsExample(new { requestId = default(string), correlationId = default(string), message = default(string) });
            errorResponse.correlationId.Should().Be(expectedCorrelationId);
            errorResponse.message.Should().Be("An error occurred in our API.  Please refer to the request id with our support team.");

            InMemorySink.Instance.Should()
                .HaveMessage("Here is an unhandled exception.")
                .Appearing().Once()
                .WithProperty("RequestId").WithValue(errorResponse.requestId);

            Log.CloseAndFlush();
        }
    }
}

namespace Weather.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Policy = "ScopePolicy")]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  private static readonly string[] Summaries =
  [
          "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  ];

  private readonly ILogger<WeatherForecastController> _logger;

  public WeatherForecastController(ILogger<WeatherForecastController> logger) => _logger = logger;

  [HttpGet(Name = "GetWeatherForecast")]
  [ProducesResponseType(StatusCodes.Status200OK)] // Documents a 200 OK response
  [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Documents a 401 Unauthorized response
  [ProducesResponseType(StatusCodes.Status403Forbidden)] // Documents a 403 Forbidden response

  public IEnumerable<WeatherForecast> Get()
  {
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
  }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace Weather.Controllers
{
    /// <summary>
    /// Ping Controller
    /// </summary>
    [Route("diagnostic")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DiagnosticController : ControllerBase
    {
        /// <summary>
        /// Get service name.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public string Get()
        {
            return "Weather Channel.";
        }

        /// <summary>
        /// Ping.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ping")]
        public string Ping()
        {
            return "pong.";
        }

        /// <summary>
        /// Throw an exception.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [Route("throw")]
        public void ThrowServerException()
        {
            Log.Error("Something bad happened");
            throw new Exception("Here is an unhandled exception.");
        }
    }
}

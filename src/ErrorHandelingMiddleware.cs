using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Weather
{
    internal class ErrorHandelingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandelingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string correlationId = CorrelationIdMiddleware.GetCorrelationId(context).Value.ToString();
            var result = new
            {
                RequestId = context.TraceIdentifier,
                CorrelationId = correlationId,
                Message = "An error occurred in our API.  Please refer to the request id with our support team.",
            };

            Log.Error(exception, exception.Message);
            var response = new ObjectResult(result)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            var actionContext = new ActionContext() { HttpContext = context };
            return response.ExecuteResultAsync(actionContext);
        }
    }
}

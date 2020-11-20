using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather
{
    internal class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private StringValues _correlationId;
        private const string CorrelationId = "CorrelationId";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context)
        {
            var correlation = GetCorrelationId(context);
            if (correlation.IsSuccess)
            {
                _correlationId = correlation.Value;
            }
            else
            {
                _correlationId = "Weather" + Guid.NewGuid();
                context.Request.Headers[CorrelationId] = _correlationId;
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(CorrelationId, _correlationId);
                return Task.CompletedTask;
            });
            return _next(context);
        }

        public static Result<StringValues> GetCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationId, out var correlationId))
            {
                return Result.Success(correlationId);
            }

            return Result.Failure<StringValues>("Correlation Id not found.");
        }
    }
}

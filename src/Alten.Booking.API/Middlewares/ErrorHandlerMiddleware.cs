using System.Diagnostics.CodeAnalysis;
using System.Net;
using Alten.Booking.Application.Extensions;
using Microsoft.AspNetCore.Diagnostics;

namespace Alten.Booking.API.Middlewares;

[ExcludeFromCodeCoverage]
public static class ErrorHandlerMiddlewareExtensions
{
    public static void UseErrorHandlerMiddleware(this IApplicationBuilder app) => app.UseExceptionHandler(_ => _.Run(
        async httpContext =>
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger>();
            // var monitoring = app.ApplicationServices.GetRequiredService<IMonitoringAdapter>();
            var exception = httpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            if (exception != null)
            {
                // logger.Error("Unexpected Error", exception);
                // monitoring.IncrementCounter(MonitoringIndicators.ExceptionCount);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(
                    exception.ToErrorViewModel(HttpStatusCode.InternalServerError));
            }
        }
    ));
}
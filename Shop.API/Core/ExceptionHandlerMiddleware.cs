using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;

namespace Shop.API.Core
{
    public static class ExceptionHandlerMiddleware
    {
        // TODO: Check exceptions
        public static void ConfigurationBuildInException(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var logger = loggerFactory.CreateLogger("ConfigurationBuildInException");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if (context != null)
                    {
                        var errVM = new Error()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeatures.Error.Message,
                            Path = contextRequest.Path
                        }.ToString();
                        
                        logger.LogError(errVM);

                        await context.Response.WriteAsync(errVM);
                    }
                });
            });
        }
    }
}
using Microsoft.AspNetCore.Builder;

namespace WebApplication.Middleware
{
    public static class ExceptionHandlerMiddleware
    {
        public static void ConfigurationBuildInException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler();
        }
    }
}
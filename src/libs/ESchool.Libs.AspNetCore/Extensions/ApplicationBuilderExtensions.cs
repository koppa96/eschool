using ESchool.Libs.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ESchool.Libs.AspNetCore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            return app;
        }
    }
}
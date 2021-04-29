using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ESchool.Libs.AspNetCore.Middlewares
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestLoggerMiddleware> logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await next(context);
            logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} Response: {context.Response.StatusCode}");
        }
    }
}
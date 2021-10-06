using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ESchool.Libs.AspNetCore.Middlewares
{
    public class ExceptionHandlerMiddleware 
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unhandled exception caught");
                
                if (e is InvalidOperationException or ArgumentException)
                {
                    context.Response.StatusCode = 400;
                    await WriteJsonAsync(context.Response, new ProblemDetails
                    {
                        Title = "Bad Request",
                        Status = 400,
                        Detail = e.Message
                    });
                }
                else if (e is UnauthorizedAccessException)
                {
                    context.Response.StatusCode = 403;
                    await WriteJsonAsync(context.Response, new ProblemDetails
                    {
                        Title = "Forbidden",
                        Status = 403,
                        Detail = e.Message
                    });
                }
                else
                {
                    context.Response.StatusCode = 500;
                    await WriteJsonAsync(context.Response, new ProblemDetails
                    {
                        Title = "Internal Server Error",
                        Status = 500,
                        Detail = e.Message
                    });
                }
            }
        }

        private Task WriteJsonAsync(HttpResponse response, object data)
        {
            return response.WriteAsync(JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}

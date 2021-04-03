using System.Threading.Tasks;
using ESchool.Libs.Outbox.Services;
using Microsoft.AspNetCore.Http;

namespace ESchool.Libs.Outbox.AspNetCore.Middlewares
{
    public class OutboxMiddleware
    {
        private readonly RequestDelegate next;

        public OutboxMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMessageDispatcher messageDispatcher)
        {
            await next(context);
        }
    }
}
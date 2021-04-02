using System;
using System.Text.Json;
using ESchool.Libs.Outbox.Models;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Entities
{
    public class OutboxEntry
    {
        public Guid Id { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public OutboxEntryState State { get; set; }
        public int Retries { get; set; }

        public static OutboxEntry FromPublishContext<TMessage>(OutboxPublishContext<TMessage> context)
        {
            return new OutboxEntry
            {
                Id = context.Id,
                Headers = JsonSerializer.Serialize(context.Headers),
                Body = JsonSerializer.Serialize(context.Message, context.Message.GetType()),
                TypeName = context.Message.GetType().AssemblyQualifiedName,
                CreatedAt = DateTime.Now,
                State = OutboxEntryState.Pending,
                Retries = 0
            };
        }
    }
}
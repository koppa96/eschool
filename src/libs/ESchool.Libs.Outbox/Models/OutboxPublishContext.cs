using System;
using System.Collections.Generic;

namespace ESchool.Libs.Outbox.Models
{
    public class OutboxPublishContext
    {
        public Dictionary<string, string> Headers { get; }
        public object Message { get; }
        public bool Canceled { get; private set; }

        public OutboxPublishContext(object message)
        {
            Headers = new Dictionary<string, string>();
            Message = message;
            Canceled = false;
        }

        public void Cancel()
        {
            Canceled = true;
        }
    }
}
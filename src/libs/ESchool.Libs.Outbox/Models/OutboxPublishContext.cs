using System;
using System.Collections.Generic;

namespace ESchool.Libs.Outbox.Models
{
    public class OutboxPublishContext<TMessage>
    {
        public Dictionary<string, string> Headers { get; set; }
        public TMessage Message { get; set; }
        public bool Canceled { get; private set; }

        public OutboxPublishContext(TMessage message)
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
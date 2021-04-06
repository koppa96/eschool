using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Commands
{
    public class DispatchSavedMessagesCommand : IRequest
    {
        public IEnumerable<Guid> MessageIds { get; set; }
    }
}
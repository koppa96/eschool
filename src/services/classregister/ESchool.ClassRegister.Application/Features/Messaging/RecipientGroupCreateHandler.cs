using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class RecipientGroupCreateCommand : IRequest
    {
        public string Name { get; set; }
    }
    
    public class RecipientGroupCreateHandler : IRequestHandler<RecipientGroupCreateCommand>
    {
        private readonly ClassRegisterContext context;

        public RecipientGroupCreateHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(RecipientGroupCreateCommand request, CancellationToken cancellationToken)
        {
            var group = new RecipientGroup
            {
                Name = request.Name
            };

            context.RecipientGroups.Add(group);
            await context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
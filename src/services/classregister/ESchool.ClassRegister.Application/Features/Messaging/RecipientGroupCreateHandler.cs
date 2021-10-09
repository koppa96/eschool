using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.Libs.Domain.Services;
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
        private readonly IIdentityService identityService;

        public RecipientGroupCreateHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<Unit> Handle(RecipientGroupCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var group = new RecipientGroup
            {
                Name = request.Name,
                UserId = currentUserId
            };

            context.RecipientGroups.Add(group);
            await context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
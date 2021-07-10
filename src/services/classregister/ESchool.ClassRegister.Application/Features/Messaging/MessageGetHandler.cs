using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Application.Features.Messaging.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Messaging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class MessageGetHandler : IRequestHandler<MessageGetQuery, MessageDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider provider;

        public MessageGetHandler(ClassRegisterContext context, IConfigurationProvider provider)
        {
            this.context = context;
            this.provider = provider;
        }
        
        public Task<MessageDetailsResponse> Handle(MessageGetQuery request, CancellationToken cancellationToken)
        {
            return context.Messages.Where(x => x.Id == request.Id)
                .ProjectTo<MessageDetailsResponse>(provider)
                .SingleAsync(cancellationToken);
        }
    }
}
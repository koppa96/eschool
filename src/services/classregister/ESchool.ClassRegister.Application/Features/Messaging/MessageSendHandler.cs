using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Messaging.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.ClassRegister.Interface.Features.Messaging;
using ESchool.Libs.Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class MessageSendValidator : AbstractValidator<MessageSendCommand>
    {
        public MessageSendValidator()
        {
            RuleFor(x => x.Subject).NotEmpty().WithMessage("A tárgy megadása kötelező.");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Az üzenet megadása kötelező.");
            RuleFor(x => x.Recipients).NotEmpty().WithMessage("Legalább egy címzett megadása kötelező.");
        }
    }
    
    public class MessageSendHandler : IRequestHandler<MessageSendCommand, MessageDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public MessageSendHandler(ClassRegisterContext context,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<MessageDetailsResponse> Handle(MessageSendCommand request, CancellationToken cancellationToken)
        {
            var groupIds = request.Recipients.Where(x => x.Type == MessageSendCommand.Recipient.RecipientType.Group)
                .Select(x => x.Id)
                .ToList();

            var groupMembersIds = await context.RecipientGroupMembers.Where(x => groupIds.Contains(x.RecipientGroupId))
                .Select(x => x.MemberId)
                .ToListAsync(cancellationToken);
            
            var message = new Message
            {
                Subject = request.Subject,
                Text = request.Text,
                SenderUserId = identityService.GetCurrentUserId(),
                SentAt = DateTime.Now,
                ReceiverUserMessages = request.Recipients
                    .Where(x => x.Type == MessageSendCommand.Recipient.RecipientType.User)
                    .Select(x => x.Id)
                    .Concat(groupMembersIds)
                    .Distinct()
                    .Select(x => new UserMessage
                    {
                        UserId = x
                    })
                    .ToList()
            };

            context.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<MessageDetailsResponse>(message);
        }
    }
}
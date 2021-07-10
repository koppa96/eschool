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

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class MessageSendValidator : AbstractValidator<MessageSendCommand>
    {
        public MessageSendValidator()
        {
            RuleFor(x => x.Subject).NotEmpty().WithMessage("A tárgy megadása kötelező.");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Az üzenet megadása kötelező.");
            RuleFor(x => x.RecipientIds).NotEmpty().WithMessage("Legalább egy címzett megadása kötelező.");
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
            var message = new Message
            {
                Subject = request.Subject,
                Text = request.Text,
                SenderUserId = identityService.GetCurrentUserId(),
                SentAt = DateTime.Now,
                ReceiverUserMessages = request.RecipientIds.Select(x => new UserMessage
                {
                    UserId = x
                }).ToList()
            };

            context.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<MessageDetailsResponse>(message);
        }
    }
}
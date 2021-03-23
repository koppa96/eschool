using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.Messaging;

namespace ESchool.ClassRegister.Application.Features.Messaging.Common
{
    public class MessageMapping : Profile
    {
        public MessageMapping()
        {
            CreateMap<Message, MessageListResponse>()
                .ForMember(x => x.Sender, o => o.MapFrom(x => x.SenderClassRegisterUser));

            CreateMap<Message, MessageDetailsResponse>()
                .ForMember(x => x.Sender, o => o.MapFrom(x => x.SenderClassRegisterUser))
                .ForMember(x => x.Recipients, o => o.MapFrom(x => x.ReceiverUserMessages.Select(r => r.ClassRegisterUser)));
        }
    }
}
using AutoMapper;
using ESchool.Messaging.Domain.Entities;

namespace ESchool.Messaging.Application.Features.RecipientGroups.Common
{
    public class RecipientGroupMapping : Profile
    {
        public RecipientGroupMapping()
        {
            CreateMap<RecipientGroup, RecipientGroupListResponse>();
        }
    }
}
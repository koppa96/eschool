using AutoMapper;
using ESchool.Libs.Interface.Response.Common;
using ESchool.Messaging.Domain.Entities;

namespace ESchool.Messaging.Application.Features.Users.Common
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<MessagingUser, UserListResponse>();
        }
    }
}
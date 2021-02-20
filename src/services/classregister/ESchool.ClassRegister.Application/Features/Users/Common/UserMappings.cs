using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.Users;

namespace ESchool.ClassRegister.Application.Features.Users.Common
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<Teacher, UserListResponse>();
        }
    }
}
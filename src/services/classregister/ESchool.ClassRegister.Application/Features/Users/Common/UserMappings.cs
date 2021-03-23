using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion;

namespace ESchool.ClassRegister.Application.Features.Users.Common
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<Teacher, UserListResponse>();

            CreateMap<Teacher, TenantUserDeletedEvent>()
                .ConstructUsing(teacher => new TeacherDeletedEvent());

            CreateMap<Student, TenantUserDeletedEvent>()
                .ConstructUsing(student => new StudentDeletedEvent());

            CreateMap<Teacher, TenantUserCreatedEvent>()
                .ConstructUsing(teacher => new TeacherCreatedEvent());

            CreateMap<Student, TenantUserCreatedEvent>()
                .ConstructUsing(student => new StudentCreatedEvent());
        }
    }
}
using AutoMapper;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Classrooms;

namespace ESchool.ClassRegister.Application.Features.Classrooms.Common
{
    public class ClassroomMappings : Profile
    {
        public ClassroomMappings()
        {
            CreateMap<Classroom, ClassroomListResponse>();
        }
    }
}
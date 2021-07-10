using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Classrooms
{
    public class ClassroomGetQuery : IRequest<ClassroomDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}
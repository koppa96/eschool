using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Classes
{
    public class ClassCreateCommand : IRequest<ClassDetailsResponse>
    {
        public Guid ClassTypeId { get; set; }
        public Guid HeadTeacherId { get; set; }
        public Guid StartingSchoolYearId { get; set; }
    }
}
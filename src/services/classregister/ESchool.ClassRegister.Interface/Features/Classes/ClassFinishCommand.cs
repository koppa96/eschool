using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Classes
{
    public class ClassFinishCommand : IRequest<ClassDetailsResponse>
    {
        public Guid ClassId { get; set; }
    }
}
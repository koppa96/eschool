using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Classes
{
    public class ClassGetQuery : IRequest<ClassDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}
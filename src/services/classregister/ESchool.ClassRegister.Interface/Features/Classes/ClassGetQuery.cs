using System;
using ESchool.ClassRegister.Interface.Features.Classes;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassGetQuery : IRequest<ClassDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}
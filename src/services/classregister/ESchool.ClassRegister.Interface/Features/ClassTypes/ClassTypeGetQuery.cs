using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.ClassTypes
{
    public class ClassTypeGetQuery : IRequest<ClassTypeDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}
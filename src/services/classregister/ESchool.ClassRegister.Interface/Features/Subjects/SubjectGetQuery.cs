using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Subjects
{
    public class SubjectGetQuery : IRequest<SubjectDetailsResponse>
    {
        public Guid Id { get; set; }        
    }
}
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectDeleteCommand : DeleteCommand
    {
    }
    
    public class SubjectDeleteHandler : DeleteHandler<SubjectDeleteCommand, Subject>
    {
        public SubjectDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}
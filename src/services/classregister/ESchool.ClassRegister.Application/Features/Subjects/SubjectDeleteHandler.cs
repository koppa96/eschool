using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectDeleteHandler : DeleteHandler<SubjectDeleteCommand, Subject>
    {
        public SubjectDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}
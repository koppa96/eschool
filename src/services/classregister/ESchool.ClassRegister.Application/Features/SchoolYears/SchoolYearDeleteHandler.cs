using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearDeleteHandler : DeleteHandler<SchoolYearDeleteCommand, SchoolYear>
    {
        public SchoolYearDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}
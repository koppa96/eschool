using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearDeleteCommand : DeleteCommand
    {
    }
    
    public class SchoolYearDeleteHandler : DeleteHandler<SchoolYearDeleteCommand, SchoolYear>
    {
        public SchoolYearDeleteHandler(DbContext context) : base(context)
        {
        }
    }
}
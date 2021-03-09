using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.GradeKinds
{
    public class GradeKindDeleteCommand : DeleteCommand
    {
    }
    
    public class GradeKindDeleteHandler : DeleteHandler<GradeKindDeleteCommand, GradeKind>
    {
        private readonly ClassRegisterContext context;

        public GradeKindDeleteHandler(ClassRegisterContext context) : base(context)
        {
            this.context = context;
        }

        protected override async Task ThrowIfCannotDeleteAsync(GradeKind entity)
        {
            if (await context.Grades.AnyAsync(x => x.KindId == entity.Id))
            {
                throw new InvalidOperationException(
                    "Nem törölhető olyan jegytípus, amelyhez már létezik jegy létrehozva.");
            }
        }
    }
}
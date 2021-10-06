using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeDeleteHandler : DeleteHandler<ClassTypeDeleteCommand, ClassType>
    {
        private readonly ClassRegisterContext context;

        public ClassTypeDeleteHandler(ClassRegisterContext context) : base(context)
        {
            this.context = context;
        }

        protected override async Task ThrowIfCannotDeleteAsync(ClassType entity)
        {
            var anyClassExistsWithClassType = await context.Classes.AnyAsync(x => x.ClassTypeId == entity.Id);
            if (anyClassExistsWithClassType)
            {
                throw new InvalidOperationException("Nem törölhető olyan tagozat, amely már osztályhoz van rendelve.");
            }
        }
    }
}
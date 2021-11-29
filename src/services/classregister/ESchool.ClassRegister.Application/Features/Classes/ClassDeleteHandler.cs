using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassDeleteHandler : DeleteHandler<ClassDeleteCommand, Class>
    {
        public ClassDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }

        protected override IQueryable<Class> Include(IQueryable<Class> entities)
        {
            return entities.Include(x => x.Students);
        }

        protected override Task ThrowIfCannotDeleteAsync(Class entity)
        {
            if (entity.Students.Any())
            {
                throw new InvalidOperationException("Can not delete a class that has students assigned.");
            }
            return Task.CompletedTask;
        }
    }
}
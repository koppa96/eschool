using System;
using System.Linq;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassDeleteCommand : DeleteCommand
    {
    }
    
    public class ClassDeleteHandler : DeleteHandler<ClassDeleteCommand, Class>
    {
        public ClassDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }

        protected override IQueryable<Class> Include(IQueryable<Class> entities)
        {
            return entities.Include(x => x.Students);
        }

        protected override void ThrowIfCannotDelete(Class entity)
        {
            if (entity.Students.Any())
            {
                throw new InvalidOperationException("Can not delete a class that has students assigned.");
            }
        }
    }
}
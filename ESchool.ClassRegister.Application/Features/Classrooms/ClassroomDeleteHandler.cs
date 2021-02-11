using System;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomDeleteCommand : DeleteCommand
    {
        public Guid Id { get; set; }
    }

    public class ClassroomDeleteHandler : DeleteHandler<ClassroomDeleteCommand, ClassRoom>
    {
        public ClassroomDeleteHandler(DbContext context) : base(context)
        {
        }
    }
}
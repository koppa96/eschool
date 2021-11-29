using System;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Classrooms;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomDeleteHandler : DeleteHandler<ClassroomDeleteCommand, Classroom>
    {
        public ClassroomDeleteHandler(ClassRegisterContext context) : base(context)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomListQuery : PagedListQuery<ClassroomListResponse>
    {
    }

    public class ClassroomListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    
    public class ClassroomListHandler : PagedListHandler<ClassroomListQuery, ClassRoom, string, ClassroomListResponse>
    {
        public ClassroomListHandler(ClassRegisterContext context) : base(context)
        {
        }

        protected override Expression<Func<ClassRoom, string>> OrderBy => x => x.Name;

        protected override Expression<Func<ClassRoom, ClassroomListResponse>> Select => x => new ClassroomListResponse
        {
            Id = x.Id,
            Name = x.Name
        };
    }
}
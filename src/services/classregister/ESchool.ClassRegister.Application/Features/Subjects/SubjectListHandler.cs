using System;
using System.Linq.Expressions;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectListQuery : PagedListQuery<SubjectListResponse>
    {
    }

    public class SubjectListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class SubjectListHandler : PagedListHandler<SubjectListQuery, Subject, string, SubjectListResponse>
    {
        public SubjectListHandler(ClassRegisterContext context) : base(context)
        {
        }

        protected override Expression<Func<Subject, string>> OrderBy => x => x.Name;

        protected override Expression<Func<Subject, SubjectListResponse>> Select =>
            x => new SubjectListResponse { Id = x.Id, Name = x.Name };
    }
}
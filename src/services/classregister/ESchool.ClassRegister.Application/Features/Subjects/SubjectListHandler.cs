using System;
using System.Linq.Expressions;
using AutoMapper;
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

    public class SubjectListHandler : AutoMapperPagedListHandler<SubjectListQuery, Subject, string, SubjectListResponse>
    {
        public SubjectListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override Expression<Func<Subject, string>> OrderBy => x => x.Name;
    }
}
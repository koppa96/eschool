using System;
using System.Linq;
using AutoMapper;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Application.Features.Tests.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestListAsStudentQuery : PagedListQuery<TestListResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
    }

    public class TestListAsStudentHandler : AutoMapperPagedListHandler<TestListAsStudentQuery, Test, TestListResponse>
    {
        private readonly IIdentityService identityService;

        public TestListAsStudentHandler(
            TestingContext context,
            IConfigurationProvider configurationProvider,
            IIdentityService identityService) : base(context, configurationProvider)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<Test> Filter(IQueryable<Test> entities, TestListAsStudentQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x => x.ClassSchoolYearSubject.ClassId == query.ClassId &&
                                       x.ClassSchoolYearSubject.SubjectId == query.SubjectId &&
                                       x.ClassSchoolYearSubject.SchoolYearId == query.SchoolYearId &&
                                       x.StudentTests.Any(st => st.Student.UserId == currentUserId));
        }

        protected override IOrderedQueryable<Test> Order(IQueryable<Test> entities)
        {
            return entities.OrderByDescending(x => x.ScheduledStart);
        }
    }
}
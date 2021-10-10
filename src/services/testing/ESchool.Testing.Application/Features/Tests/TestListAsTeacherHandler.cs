using System.Linq;
using AutoMapper;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Application.Features.Tests.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Interface.Features.Tests;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestListAsTeacherHandler : AutoMapperPagedListHandler<TestListAsTeacherQuery, Test, TestListResponse>
    {
        private readonly IIdentityService identityService;

        public TestListAsTeacherHandler(
            TestingContext context,
            IConfigurationProvider configurationProvider,
            IIdentityService identityService) : base(context, configurationProvider)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<Test> Filter(IQueryable<Test> entities, TestListAsTeacherQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x => x.ClassSchoolYearSubject.Class.Id == query.ClassId &&
                                       x.ClassSchoolYearSubject.Subject.Id == query.SubjectId &&
                                       x.ClassSchoolYearSubject.SchoolYear.Id == query.SchoolYearId &&
                                       x.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Any(t =>
                                           t.Teacher.UserId == currentUserId));
        }

        protected override IOrderedQueryable<Test> Order(IQueryable<Test> entities, TestListAsTeacherQuery query)
        {
            return entities.OrderByDescending(x => x.ScheduledStart);
        }
    }
}
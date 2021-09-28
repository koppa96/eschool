using System;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSubjects
{
    public class ClassSubjectListQuery : PagedListQuery<ClassSubjectListResponse>
    {
        public Guid SchoolYearId { get; set; }
    }

    public class ClassSubjectListResponse
    {
        public ClassListResponse Class { get; set; }
        public SubjectListResponse Subject { get; set; }
    }
    
    public class ClassSubjectListHandler : AutoMapperPagedListHandler<ClassSubjectListQuery, ClassSchoolYearSubject, ClassSubjectListResponse>
    {
        private readonly IIdentityService identityService;

        public ClassSubjectListHandler(
            ClassRegisterContext context,
            IIdentityService identityService,
            IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<ClassSchoolYearSubject> Filter(IQueryable<ClassSchoolYearSubject> entities, ClassSubjectListQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x =>
                x.ClassSchoolYear.SchoolYearId == query.SchoolYearId &&
                x.ClassSchoolYearSubjectTeachers.Any(t => t.Teacher.UserId == currentUserId));
        }

        protected override IOrderedQueryable<ClassSchoolYearSubject> Order(IQueryable<ClassSchoolYearSubject> entities)
        {
            return entities.OrderBy(x =>
                    x.ClassSchoolYear.Class.ClassType.StartingGrade + x.ClassSchoolYear.Class.ClassSchoolYears.Count)
                .ThenBy(x => x.ClassSchoolYear.Class.ClassType.Name)
                .ThenBy(x => x.Subject.Name);
        }
    }
}
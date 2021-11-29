using System;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class StudentLessonListQuery : PagedListQuery<LessonListResponse>
    {
        public Guid StudentId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
    }

    public class StudentLessonListHandler : AutoMapperPagedListHandler<StudentLessonListQuery, Lesson, LessonListResponse>
    {
        public StudentLessonListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
            : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Lesson> Filter(IQueryable<Lesson> entities, StudentLessonListQuery query)
        {
            return entities.Where(x => x.ClassSchoolYearSubject.SubjectId == query.SubjectId &&
                                       x.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId == query.SchoolYearId &&
                                       x.ClassSchoolYearSubject.ClassSchoolYear.Class.Students.Any(s =>
                                           s.Id == query.StudentId));
        }

        protected override IOrderedQueryable<Lesson> Order(IQueryable<Lesson> entities, StudentLessonListQuery query)
        {
            return entities.OrderBy(x => x.StartsAt);
        }
    }
}
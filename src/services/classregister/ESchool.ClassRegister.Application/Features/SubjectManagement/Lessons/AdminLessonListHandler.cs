using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class AdminLessonListHandler : AutoMapperPagedListHandler<AdminLessonListQuery, Lesson, LessonListResponse>
    {
        public AdminLessonListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Lesson> Filter(IQueryable<Lesson> entities, AdminLessonListQuery query)
        {
            return entities.Where(x =>
                x.ClassSchoolYearSubject.ClassSchoolYear.ClassId == query.ClassId &&
                x.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId == query.SchoolYearId &&
                x.ClassSchoolYearSubject.SubjectId == query.SubjectId);
        }

        protected override IOrderedQueryable<Lesson> Order(IQueryable<Lesson> entities, AdminLessonListQuery query)
        {
            return entities.OrderBy(x => x.StartsAt);
        }
    }
}
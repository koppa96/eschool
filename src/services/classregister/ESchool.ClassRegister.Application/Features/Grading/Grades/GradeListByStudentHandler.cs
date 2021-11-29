using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeListByStudentHandler : AutoMapperPagedListHandler<GradeListByStudentQuery, Grade, GradeListResponse>
    {
        public GradeListByStudentHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
            : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Grade> Filter(IQueryable<Grade> entities, GradeListByStudentQuery query)
        {
            return entities.Where(x =>
                x.Student.Id == query.StudentId &&
                x.ClassSchoolYearSubject.SubjectId == query.SubjectId &&
                x.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId == query.SchoolYearId);
        }

        protected override IOrderedQueryable<Grade> Order(IQueryable<Grade> entities, GradeListByStudentQuery query)
        {
            return entities.OrderByDescending(x => x.WrittenIn);
        }
    }
}
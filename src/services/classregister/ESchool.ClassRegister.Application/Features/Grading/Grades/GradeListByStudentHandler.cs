using System.Linq;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeListByStudentHandler : PagedListHandler<GradeListByStudentQuery, Grade, GradeListByStudentResponse>
    {
        public GradeListByStudentHandler(ClassRegisterContext context) : base(context)
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
            return entities.OrderBy(x => x.ClassSchoolYearSubject.Subject.Name);
        }

        protected override IQueryable<GradeListByStudentResponse> Map(IQueryable<Grade> entities, GradeListByStudentQuery query)
        {
            return entities.GroupBy(x => x.ClassSchoolYearSubject.Subject)
                .Select(x => new GradeListByStudentResponse
                {
                    Subject = new SubjectListResponse
                    {
                        Id = x.Key.Id,
                        Name = x.Key.Name
                    },
                    Grades = x.Select(g => new GradeListResponse
                        {
                            Id = g.Id,
                            Value = g.Value,
                            GradeKind = new GradeKindResponse
                            {
                                Id = g.Kind.Id,
                                Name = g.Kind.Name,
                                AverageMultiplier = g.Kind.AverageMultiplier
                            }
                        })
                        .ToList()
                });
        }
    }
}
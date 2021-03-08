using System;
using System.Linq;
using ESchool.ClassRegister.Application.Features.Subjects;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;

namespace ESchool.ClassRegister.Application.Features.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectListQuery : PagedListQuery<SubjectListResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
    }

    public class ClassSchoolYearSubjectListHandler : PagedListHandler<ClassSchoolYearSubjectListQuery, ClassSchoolYearSubject, SubjectListResponse>
    {
        public ClassSchoolYearSubjectListHandler(ClassRegisterContext context) : base(context)
        {
        }

        protected override IQueryable<ClassSchoolYearSubject> Filter(IQueryable<ClassSchoolYearSubject> entities, ClassSchoolYearSubjectListQuery query)
        {
            return entities.Where(x => x.SubjectId == query.SubjectId &&
                                       x.ClassSchoolYear.ClassId == query.ClassId &&
                                       x.ClassSchoolYear.SchoolYearId == query.SchoolYearId);
        }

        protected override IQueryable<SubjectListResponse> Map(IQueryable<ClassSchoolYearSubject> entities, ClassSchoolYearSubjectListQuery query)
        {
            return entities.Select(x => new SubjectListResponse
            {
                Id = x.Subject.Id,
                Name = x.Subject.Name
            });
        }

        protected override IOrderedQueryable<ClassSchoolYearSubject> Order(IQueryable<ClassSchoolYearSubject> entities)
        {
            return entities.OrderBy(x => x.Subject.Name);
        }
    }
}
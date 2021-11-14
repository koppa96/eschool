using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Students;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.Students
{
    public class
        StudentSubjectListHandler : AutoMapperPagedListHandler<StudentSubjectListQuery, Subject, SubjectListResponse>
    {
        public StudentSubjectListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
            : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Subject> Filter(IQueryable<Subject> entities, StudentSubjectListQuery query)
        {
            return entities.Where(x =>
                x.ClassSchoolYearSubjects.Any(x => x.ClassSchoolYear.SchoolYearId == query.SchoolYearId) &&
                x.ClassSchoolYearSubjects.SelectMany(x => x.ClassSchoolYear.Class.Students)
                    .Any(s => s.Id == query.StudentId));
        }

        protected override IOrderedQueryable<Subject> Order(IQueryable<Subject> entities, StudentSubjectListQuery query)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}
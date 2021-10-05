using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeListByClassSchoolYearSubjectHandler : PagedListHandler<GradeListByClassSchoolYearSubjectQuery,
        Student,
        GradeListByClassSchoolYearSubjectResponse>
    {
        private readonly ClassRegisterContext context;

        public GradeListByClassSchoolYearSubjectHandler(ClassRegisterContext context) : base(context)
        {
            this.context = context;
        }
        
        protected override IOrderedQueryable<Student> Order(IQueryable<Student> entities, GradeListByClassSchoolYearSubjectQuery query)
        {
            return entities.OrderBy(x => x.User.Name);
        }
        
        protected override IQueryable<Student> Filter(IQueryable<Student> entities, GradeListByClassSchoolYearSubjectQuery query)
        {
            return entities.Where(x => x.ClassId == query.ClassId);
        }

        protected override IQueryable<GradeListByClassSchoolYearSubjectResponse> Map(IQueryable<Student> entities, GradeListByClassSchoolYearSubjectQuery query)
        {
            return entities.Select(x => new GradeListByClassSchoolYearSubjectResponse
            {
                Student = new UserRoleListResponse
                {
                    Id = x.Id,
                    Name = x.User.Name
                },
                Grades = x.Grades.Where(g => g.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId == query.SchoolYearId &&
                                             g.ClassSchoolYearSubject.SubjectId == query.SubjectId)
                    .OrderBy(g => g.WrittenIn)
                    .Select(g => new GradeListResponse
                    {
                        Id = g.Id,
                        Value = g.Value,
                        WrittenIn = g.WrittenIn,
                        GradeKind = new GradeKindResponse
                        {
                            Id = g.Kind.Id,
                            AverageMultiplier = g.Kind.AverageMultiplier,
                            Name = g.Kind.Name
                        }
                    })
                    .ToList()
            });
        }
    }
}
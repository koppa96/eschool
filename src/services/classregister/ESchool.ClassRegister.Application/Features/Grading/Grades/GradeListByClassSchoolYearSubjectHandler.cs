using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeListByClassSchoolYearSubjectHandler : IRequestHandler<GradeListByClassSchoolYearSubjectQuery,
        List<GradeListByClassSchoolYearSubjectResponse>>
    {
        private readonly ClassRegisterContext context;

        public GradeListByClassSchoolYearSubjectHandler(ClassRegisterContext context)
        {
            this.context = context;
        }

        public async Task<List<GradeListByClassSchoolYearSubjectResponse>> Handle(
            GradeListByClassSchoolYearSubjectQuery request, CancellationToken cancellationToken)
        {
            return await context.Students.Where(x => x.ClassId == request.ClassId)
                .OrderBy(x => x.User.Name)
                .Select(x => new GradeListByClassSchoolYearSubjectResponse
                {
                    Student = new UserRoleListResponse
                    {
                        Id = x.Id,
                        Name = x.User.Name
                    },
                    Grades = x.Grades.Where(g => g.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId ==
                                                 request.SchoolYearId &&
                                                 g.ClassSchoolYearSubject.SubjectId == request.SubjectId)
                        .OrderBy(g => g.WrittenIn)
                        .Select(g => new GradeListResponse
                        {
                            Id = g.Id,
                            Value = g.Value,
                            GradeKind = new GradeKindResponse
                            {
                                Id = g.Kind.Id,
                                AverageMultiplier = g.Kind.AverageMultiplier,
                                Name = g.Kind.Name
                            }
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common;
using ESchool.ClassRegister.Application.Features.Grading.Grades.Common;
using ESchool.ClassRegister.Application.Features.Subjects;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeListByStudentQuery : IRequest<List<GradeListByStudentResponse>>
    {
        public Guid StudentId { get; set; }
        public Guid SchoolYearId { get; set; }
    }

    public class GradeListByStudentResponse
    {
        public SubjectListResponse Subject { get; set; }
        public List<GradeListResponse> Grades { get; set; }
    }

    public class GradeListByStudentHandler : IRequestHandler<GradeListByStudentQuery, List<GradeListByStudentResponse>>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public GradeListByStudentHandler(ClassRegisterContext context)
        {
            this.context = context;
        }

        public async Task<List<GradeListByStudentResponse>> Handle(GradeListByStudentQuery request,
            CancellationToken cancellationToken)
        {
            var grades = await context.Grades.Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Kind)
                .Where(x =>
                    x.StudentId == request.StudentId &&
                    x.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId == request.SchoolYearId)
                .ToListAsync(cancellationToken);

            return grades.GroupBy(x => x.ClassSchoolYearSubject.Subject)
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
                })
                .ToList();
        }
    }
}
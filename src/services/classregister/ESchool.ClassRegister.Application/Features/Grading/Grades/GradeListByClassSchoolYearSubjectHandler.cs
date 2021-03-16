using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common;
using ESchool.ClassRegister.Application.Features.Grading.Grades.Common;
using ESchool.ClassRegister.Application.Features.Users.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeListByClassSchoolYearSubjectQuery : IRequest<List<GradeListByClassSchoolYearSubjectResponse>>
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
    }

    public class GradeListByClassSchoolYearSubjectResponse
    {
        public UserListResponse Student { get; set; }
        public List<GradeListResponse> Grades { get; set; }
    }

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
                .OrderBy(x => x.Name)
                .Select(x => new GradeListByClassSchoolYearSubjectResponse
                {
                    Student = new UserListResponse
                    {
                        Id = x.Id,
                        Name = x.Name
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
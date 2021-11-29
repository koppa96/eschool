using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeCreateHandler : IRequestHandler<GradeCreateCommand, GradeDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public GradeCreateHandler(ClassRegisterContext context, IIdentityService identityService, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<GradeDetailsResponse> Handle(GradeCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var student = await context.Students.Include(x => x.Class)
                    .ThenInclude(x => x.ClassSchoolYears.Where(x => x.SchoolYearId == request.SchoolYearId))
                        .ThenInclude(x => x.ClassSchoolYearSubjects.Where(x => x.SubjectId == request.SubjectId))
                            .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                .Include(x => x.Class)
                    .ThenInclude(x => x.ClassSchoolYears.Where(x => x.SchoolYearId == request.SchoolYearId))
                        .ThenInclude(x => x.ClassSchoolYearSubjects.Where(x => x.SubjectId == request.SubjectId))
                            .ThenInclude(x => x.Subject)
                .Include(x => x.User)
                .SingleAsync(x => x.Id == request.StudentId, cancellationToken);

            var classSchoolYearSubject = student.Class.ClassSchoolYears.Single().ClassSchoolYearSubjects.Single();
            
            var teacher = await context.Teachers.Include(x => x.User)
                .SingleAsync(x => x.UserId == currentUserId, cancellationToken);
            
            var gradeKind = await context.GradeKinds.FindOrThrowAsync(request.GradeKindId, cancellationToken);
            var grade = new Grade
            {
                Value = request.Value,
                Description = request.Description,
                Kind = gradeKind,
                ClassSchoolYearSubject = classSchoolYearSubject,
                Student = student,
                Teacher = teacher,
                WrittenIn = request.WrittenIn
            };

            context.Grades.Add(grade);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<GradeDetailsResponse>(grade);
        }
    }
}
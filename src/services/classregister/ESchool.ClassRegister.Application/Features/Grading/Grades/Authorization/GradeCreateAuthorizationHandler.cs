using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades.Authorization
{
    public class GradeCreateAuthorizationHandler : IRequestAuthorizationHandler<GradeCreateCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public GradeCreateAuthorizationHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(GradeCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var classSchoolYearSubjectTeachers = await context.ClassSchoolYearSubjectTeachers
                .Include(x => x.Teacher)
                .Where(x => x.ClassSchoolYearSubject.SubjectId == request.SubjectId &&
                            x.ClassSchoolYearSubject.ClassSchoolYear.Class.Students.Any(s => s.Id == request.StudentId) &&
                            x.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId == request.SchoolYearId)
                .ToListAsync(cancellationToken);
            
            if (classSchoolYearSubjectTeachers.All(x => x.Teacher.UserId != currentUserId))
            {
                return RequestAuthorizationResult.Failure(
                    "Csak olyan tanár írhat be jegyet, aki tanítja az adott tárgyat az adott tanévben az osztálynak.");
            }
            
            return RequestAuthorizationResult.Success;
        }
    }
}
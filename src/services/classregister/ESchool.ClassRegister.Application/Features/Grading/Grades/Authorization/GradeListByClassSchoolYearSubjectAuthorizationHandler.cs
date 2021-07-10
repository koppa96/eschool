using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades.Authorization
{
    public class GradeListByClassSchoolYearSubjectAuthorizationHandler : IRequestAuthorizationHandler<GradeListByClassSchoolYearSubjectQuery>
    {
        private readonly IIdentityService identityService;
        private readonly ClassRegisterContext context;

        public GradeListByClassSchoolYearSubjectAuthorizationHandler(IIdentityService identityService,
            ClassRegisterContext context)
        {
            this.identityService = identityService;
            this.context = context;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(GradeListByClassSchoolYearSubjectQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            if (identityService.IsInRole(TenantRoleType.Administrator))
            {
                return RequestAuthorizationResult.Success;
            }

            var classSchoolYearSubjectTeachers = await context.ClassSchoolYearSubjectTeachers.Include(x => x.Teacher)
                .Where(x => x.ClassSchoolYearSubject.SubjectId == request.SubjectId &&
                            x.ClassSchoolYearSubject.ClassSchoolYear.ClassId == request.ClassId &&
                            x.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId == request.SchoolYearId)
                .ToListAsync(cancellationToken);

            if (classSchoolYearSubjectTeachers.Any(x => x.Teacher.UserId == currentUserId))
            {
                return RequestAuthorizationResult.Success;
            }

            var headTeacher = await context.Teachers.SingleAsync(x => x.CurrentClassId == request.ClassId, cancellationToken);
            if (headTeacher.UserId == currentUserId)
            {
                return RequestAuthorizationResult.Success;
            }
            
            return RequestAuthorizationResult.Failure(
                "Egy osztály jegyeit egy tárgyból csak az adminisztrátorok, a tárgyat tanító tanárok és az osztályfőnök tekintheti meg.");
        }
    }
}
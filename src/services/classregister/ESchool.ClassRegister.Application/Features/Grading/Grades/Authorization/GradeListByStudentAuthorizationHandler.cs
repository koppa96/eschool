using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades.Authorization
{
    public class GradeListByStudentAuthorizationHandler : IRequestAuthorizationHandler<GradeListByStudentQuery>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public GradeListByStudentAuthorizationHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(GradeListByStudentQuery request, CancellationToken cancellationToken)
        {
            if (identityService.IsInRole(TenantRoleType.Administrator))
            {
                return RequestAuthorizationResult.Success;
            }
            
            var currentUserId = identityService.GetCurrentUserId();
            var student = await context.Students.Include(x => x.StudentParents)
                .ThenInclude(x => x.Parent)
                .Include(x => x.Class)
                .ThenInclude(x => x.HeadTeacher)
                .SingleAsync(x => x.Id == request.StudentId, cancellationToken);
            
            if (currentUserId == student.UserId ||
                currentUserId == student.Class.HeadTeacher.UserId ||
                student.StudentParents.Any(x => x.Parent.UserId == currentUserId))
            {
                return RequestAuthorizationResult.Success;
            }

            return RequestAuthorizationResult.Failure(
                "Egy diák jegyeit csak az adminisztrátorok, osztályfőnöke, szülei és ő maga tekinthetik meg.");
        }
    }
}
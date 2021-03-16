using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences.Authorization
{
    public class AbsenceListAuthorizationHandler : IRequestAuthorizationHandler<AbsenceListQuery>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public AbsenceListAuthorizationHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(AbsenceListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var student = await context.Students.Include(x => x.Class)
                    .ThenInclude(x => x.HeadTeacher)
                .Include(x => x.StudentParents)
                    .ThenInclude(x => x.Parent)
                .SingleAsync(x => x.Id == request.StudentId, cancellationToken);
            
            if (identityService.IsInRole(TenantRoleType.Administrator) ||
                currentUserId == student.UserId ||
                student.Class.HeadTeacher.UserId == currentUserId ||
                student.StudentParents.Any(x => x.Parent.UserId == currentUserId))
            {
                return RequestAuthorizationResult.Success;
            }
            
            return RequestAuthorizationResult.Failure(
                "Egy diák hiányzásait csak az osztályfőnök, a diák, a szülei és az adminisztrátorok tekinthetik meg.");
        }
    }
}
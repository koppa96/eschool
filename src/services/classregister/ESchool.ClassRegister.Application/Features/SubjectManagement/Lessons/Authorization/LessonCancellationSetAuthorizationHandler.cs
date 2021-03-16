using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Authorization
{
    public class LessonCancellationSetAuthorizationHandler : IRequestAuthorizationHandler<EditCommand<LessonCancellationSetCommand>>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public LessonCancellationSetAuthorizationHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(EditCommand<LessonCancellationSetCommand> request, CancellationToken cancellationToken)
        {
            if (!identityService.IsInRole(TenantRoleType.Administrator) &&
                !identityService.IsInRole(TenantRoleType.Teacher))
            {
                return RequestAuthorizationResult.Failure("Csak adminisztrátorok és tanárok állíthatják az óra állapotát.");
            }

            var currentUserId = identityService.GetCurrentUserId();
            var lesson = await context.Lessons.Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                        .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            
            
            if (identityService.IsInRole(TenantRoleType.Teacher) && lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.All(x =>
                x.Teacher.UserId != currentUserId))
            {
                return RequestAuthorizationResult.Failure("Csak olyan tanár állíthatja az óra állapotát aki az adott tárgyat tartja.");
            }
            
            return RequestAuthorizationResult.Success;
        }
    }
}
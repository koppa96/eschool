using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonCancellationSetCommand
    {
        public bool Canceled { get; set; }
    }
    
    public class LessonCancellationSetHandler : IRequestHandler<EditCommand<LessonCancellationSetCommand, LessonDetailsResponse>, LessonDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public LessonCancellationSetHandler(ClassRegisterContext context,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<LessonDetailsResponse> Handle(EditCommand<LessonCancellationSetCommand, LessonDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                .Include(x => x.Classroom)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (!identityService.IsInRole(TenantRoleType.Administrator) &&
                !identityService.IsInRole(TenantRoleType.Teacher))
            {
                throw new UnauthorizedAccessException("Csak adminisztrátorok és tanárok állíthatják az óra állapotát.");
            }

            var currentUserId = identityService.GetCurrentUserId();
            if (identityService.IsInRole(TenantRoleType.Teacher) && lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.All(x =>
                x.TeacherId != currentUserId))
            {
                throw new UnauthorizedAccessException(
                    "Csak olyan tanár állíthatja az óra állapotát aki az adott tárgyat tartja.");
            }

            lesson.Canceled = request.InnerCommand.Canceled;
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<LessonDetailsResponse>(lesson);
        }
    }
}
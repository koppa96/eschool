using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkStudentAndTeacherListRefreshCommand : IRequest
    {
        public Guid HomeworkId { get; set; }
    }
    
    public class HomeworkStudentAndTeacherListRefreshHandler : IRequestHandler<HomeworkStudentAndTeacherListRefreshCommand>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;
        private readonly LessonService.LessonServiceClient client;

        public HomeworkStudentAndTeacherListRefreshHandler(HomeAssignmentsContext context,
            IIdentityService identityService,
            LessonService.LessonServiceClient client)
        {
            this.context = context;
            this.identityService = identityService;
            this.client = client;
        }
        
        public async Task<Unit> Handle(HomeworkStudentAndTeacherListRefreshCommand request, CancellationToken cancellationToken)
        {
            var homework = await context.Homeworks.FindOrThrowAsync(request.HomeworkId, cancellationToken);

            var response = await client.ListStudentsAndTeachersForLessonAsync(new StudentAndTeacherListRequest
            {
                LessonId = homework.LessonId.ToString()
            }, cancellationToken: cancellationToken);
            
            var studentGuids
        }
    }
}
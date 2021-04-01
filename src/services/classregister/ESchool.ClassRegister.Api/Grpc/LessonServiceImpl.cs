using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons;
using ESchool.ClassRegister.Grpc;
using Grpc.Core;
using MediatR;

namespace ESchool.ClassRegister.Api.Grpc
{
    public class LessonServiceImpl : LessonService.LessonServiceBase
    {
        private readonly IMediator mediator;

        public LessonServiceImpl(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<LessonInfoForHomeAssignmentsResponse> GetLessonInfoForHomeAssignments(
            LessonInfoForHomeAssignmentsRequest request,
            ServerCallContext context)
        {
            var lessonDetails = await mediator.Send(new LessonInfoQuery
            {
                Id = Guid.Parse(request.Id)
            });

            return new LessonInfoForHomeAssignmentsResponse
            {
                Id = lessonDetails.ToString(),
                Title = lessonDetails.Title,
                ClassSchoolYearSubjectId = lessonDetails.ClassSchoolYearSubjectId.ToString()
            };
        }
    }
}
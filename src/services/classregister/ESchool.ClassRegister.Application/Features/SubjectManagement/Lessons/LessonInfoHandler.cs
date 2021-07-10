using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.Libs.Domain.Extensions;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonInfoHandler : IRequestHandler<LessonInfoQuery, LessonInfoResponse>
    {
        private readonly ClassRegisterContext context;

        public LessonInfoHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<LessonInfoResponse> Handle(LessonInfoQuery request, CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.FindOrThrowAsync(request.Id, cancellationToken);
            return new LessonInfoResponse
            {
                Id = lesson.Id,
                Title = lesson.Title,
                ClassSchoolYearSubjectId = lesson.ClassSchoolYearSubjectId
            };
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectLessonsEndpoint : LessonsEndpoint
    {
        private readonly string baseUrl;
        private readonly IFlurlClient flurlClient;

        public ClassSchoolYearSubjectLessonsEndpoint(string baseUrl, IFlurlClient flurlClient)
        {
            this.baseUrl = baseUrl;
            this.flurlClient = flurlClient;
        }

        public Task<LessonDetailsResponse> CreateAsync(LessonCreateCommand.LessonCreateCommandBody command,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(baseUrl)
                .PostJsonAsync(command, cancellationToken)
                .ReceiveJson<LessonDetailsResponse>();
        }

        public Task AddAbsenceAsync(Guid lessonId, Guid studentId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(baseUrl, lessonId, "absences", studentId)
                .PostAsync(cancellationToken: cancellationToken);
        }

        public Task<LessonDetailsResponse> EditAsync(Guid lessonId, LessonEditCommand command, CancellationToken cancellationToken)
        {
            return flurlClient.Request(baseUrl, lessonId)
                .PutJsonAsync(command, cancellationToken)
                .ReceiveJson<LessonDetailsResponse>();
        }

        public Task<LessonDetailsResponse> SetCancellationAsync(Guid lessonId, bool canceled,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(baseUrl, lessonId)
                .PatchJsonAsync(new LessonCancellationSetCommand { Canceled = canceled }, cancellationToken)
                .ReceiveJson<LessonDetailsResponse>();
        }

        public Task DeleteAsync(Guid lessonId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(baseUrl, lessonId)
                .DeleteAsync(cancellationToken);
        }
    }
}
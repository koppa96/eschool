using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects.ClassSchoolYearSubjectStudents;
using ESchool.Libs.Interface.Response;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectsEndpoint
    {
        private readonly string basePath;
        private readonly IFlurlClient flurlClient;
        private readonly ChildEndpointFactory childEndpointFactory;

        public ClassSchoolYearSubjectsChildEndpointSelector this[Guid subjectId] =>
            childEndpointFactory.CreateChildEndpointSelector<ClassSchoolYearSubjectsChildEndpointSelector>(
                basePath + $"/{subjectId}");
        
        public ClassSchoolYearSubjectsEndpoint(
            string basePath,
            IFlurlClient flurlClient,
            ChildEndpointFactory childEndpointFactory)
        {
            this.basePath = basePath;
            this.flurlClient = flurlClient;
            this.childEndpointFactory = childEndpointFactory;
        }

        public Task<PagedListResponse<SubjectListResponse>> GetPagedListAsync(
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath)
                .GetJsonAsync<PagedListResponse<SubjectListResponse>>(cancellationToken);
        }

        public Task CreateAsync(
            Guid subjectId,
            List<Guid> teacherIds,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, subjectId)
                .PostJsonAsync(teacherIds, cancellationToken);
        }

        public Task EditAsync(
            Guid subjectId,
            List<Guid> teacherIds,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, subjectId)
                .PutJsonAsync(teacherIds, cancellationToken);
        }

        public Task DeleteAsync(
            Guid subjectId,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(basePath, subjectId)
                .DeleteAsync(cancellationToken);
        }
    }
}
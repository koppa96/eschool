using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects;
using Grpc.Core;
using MediatR;
using ClassSchoolYearSubjectDetailsResponse = ESchool.ClassRegister.Grpc.ClassSchoolYearSubjectDetailsResponse;

namespace ESchool.ClassRegister.Api.Grpc
{
    public class ClassSchoolYearSubjectServiceImpl : ClassSchoolYearSubjectService.ClassSchoolYearSubjectServiceBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectServiceImpl(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public override async Task<ClassSchoolYearSubjectDetailsResponse> GetDetails(ClassSchoolYearSubjectDetailsRequest request, ServerCallContext context)
        {
            var result = await mediator.Send(new ClassSchoolYearSubjectQuery
            {
                ClassId = Guid.Parse(request.ClassId),
                SchoolYearId = Guid.Parse(request.SchoolYearId),
                SubjectId = Guid.Parse(request.SubjectId)
            });

            var response = new ClassSchoolYearSubjectDetailsResponse
            {
                Class = new ClassRegisterEntityResponse
                {
                    Id = result.Class.Id.ToString(),
                    Name = $"{result.Class.Grade}. {result.Class.ClassType.Name}"
                },
                Subject = new ClassRegisterEntityResponse
                {
                    Id = result.Subject.Id.ToString(),
                    Name = result.Subject.Name
                },
                SchoolYear = new ClassRegisterEntityResponse
                {
                    Id = result.SchoolYear.Id.ToString(),
                    Name = result.SchoolYear.DisplayName
                },
            };
            
            response.TeacherIds.AddRange(result.Teachers.Select(x => x.Id.ToString()));
            response.StudentIds.AddRange(result.Students.Select(x => x.Id.ToString()));

            return response;
        }
    }
}
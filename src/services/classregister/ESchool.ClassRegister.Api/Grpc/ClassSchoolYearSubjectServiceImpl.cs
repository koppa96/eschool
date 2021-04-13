using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using Grpc.Core;
using MediatR;

namespace ESchool.ClassRegister.Api.Grpc
{
    public class ClassSchoolYearSubjectServiceImpl : ClassSchoolYearSubjectService.ClassSchoolYearSubjectServiceBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectServiceImpl(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public override Task<ClassSchoolYearSubjectDetailsResponse> GetDetails(ClassSchoolYearSubjectDetailsRequest request, ServerCallContext context)
        {
            return base.GetDetails(request, context);
        }
    }
}
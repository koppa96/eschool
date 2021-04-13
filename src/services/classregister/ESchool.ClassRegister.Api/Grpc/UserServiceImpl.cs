using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using Grpc.Core;
using MediatR;

namespace ESchool.ClassRegister.Api.Grpc
{
    public class UserServiceImpl : UserService.UserServiceBase
    {
        private readonly IMediator mediator;

        public UserServiceImpl(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public override Task<UserListResponse> GetStudentOfClass(StudentsOfClassRequest request, ServerCallContext context)
        {
            return base.GetStudentOfClass(request, context);
        }

        public override Task<UserListResponse> GetTeachersOfClassSchoolYearSubject(TeachersOfClassSchoolYearSubjectRequest request, ServerCallContext context)
        {
            return base.GetTeachersOfClassSchoolYearSubject(request, context);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Classrooms;
using ESchool.Libs.Domain.Extensions;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomGetHandler : IRequestHandler<ClassroomGetQuery, ClassroomDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public ClassroomGetHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<ClassroomDetailsResponse> Handle(ClassroomGetQuery request, CancellationToken cancellationToken)
        {
            var classroom = await context.Classrooms.FindOrThrowAsync(request.Id, cancellationToken);
            return new ClassroomDetailsResponse
            {
                Id = classroom.Id,
                Name = classroom.Name
            };
        }
    }
}
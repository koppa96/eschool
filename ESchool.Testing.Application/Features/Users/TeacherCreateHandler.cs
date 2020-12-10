using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.IntegrationEvents.UserCreation;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using MediatR;

namespace ESchool.Testing.Application.Features.Users
{
    public class TeacherCreateHandler : IRequestHandler<TeacherCreatedIntegrationEvent, Unit>
    {
        private readonly TestingContext context;

        public TeacherCreateHandler(TestingContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(TeacherCreatedIntegrationEvent request, CancellationToken cancellationToken)
        {
            context.Teachers.Add(new Teacher
            {
                Id = request.Id,
                Name = request.Name,
                UserId = request.UserId,
                TenantId = request.TenantId
            });

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
using ESchool.Libs.Application.IntegrationEvents.UserCreation;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ESchool.Testing.Application.Features.Users
{
    public class StudentCreateHandler : IRequestHandler<StudentCreatedIntegrationEvent, Unit>
    {
        private readonly TestingContext context;

        public StudentCreateHandler(TestingContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(StudentCreatedIntegrationEvent request, CancellationToken cancellationToken)
        {
            context.Students.Add(new Student
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
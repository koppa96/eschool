using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.IntegrationEvents;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using MediatR;

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
                Name = request.Name
            });

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
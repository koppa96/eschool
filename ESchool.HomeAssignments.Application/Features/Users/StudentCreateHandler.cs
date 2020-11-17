using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents;
using MediatR;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class StudentCreateHandler : IRequestHandler<StudentCreatedIntegrationEvent, Unit>
    {
        private readonly HomeAssignmentsContext context;

        public StudentCreateHandler(HomeAssignmentsContext context)
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
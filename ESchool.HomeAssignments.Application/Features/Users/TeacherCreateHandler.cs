using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents;
using MediatR;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class TeacherCreateHandler : IRequestHandler<TeacherCreatedIntegrationEvent, Unit>
    {
        private readonly HomeAssignmentsContext context;

        public TeacherCreateHandler(HomeAssignmentsContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(TeacherCreatedIntegrationEvent request, CancellationToken cancellationToken)
        {
            context.Teachers.Add(new Teacher
            {
                Id = request.Id,
                Name = request.Name
            });

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
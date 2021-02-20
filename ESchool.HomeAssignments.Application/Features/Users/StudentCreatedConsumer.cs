using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using MassTransit;
using MediatR;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class StudentCreatedConsumer : IConsumer<StudentCreatedIntegrationEvent>
    {
        private readonly HomeAssignmentsContext dbContext;

        public StudentCreatedConsumer(HomeAssignmentsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<StudentCreatedIntegrationEvent> context)
        {
            dbContext.Students.Add(new Student
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                UserId = context.Message.UserId,
                TenantId = context.Message.TenantId
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
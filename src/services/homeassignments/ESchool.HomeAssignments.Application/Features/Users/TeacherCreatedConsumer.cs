using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using MassTransit;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class TeacherCreatedConsumer : IConsumer<TeacherCreatedEvent>
    {
        private readonly HomeAssignmentsContext dbContext;

        public TeacherCreatedConsumer(HomeAssignmentsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<TeacherCreatedEvent> context)
        {
            dbContext.Teachers.Add(new Teacher
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
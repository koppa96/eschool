using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using MassTransit;

namespace ESchool.Testing.Application.Features.Users
{
    public class TeacherCreatedConsumer : IConsumer<TeacherCreatedOrUpdatedEvent>
    {
        private readonly TestingContext dbContext;

        public TeacherCreatedConsumer(TestingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<TeacherCreatedOrUpdatedEvent> context)
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
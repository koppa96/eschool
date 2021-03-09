using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using MassTransit;

namespace ESchool.Testing.Application.Features.Users
{
    public class StudentCreatedConsumer : IConsumer<StudentCreatedEvent>
    {
        private readonly TestingContext dbContext;

        public StudentCreatedConsumer(TestingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<StudentCreatedEvent> context)
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
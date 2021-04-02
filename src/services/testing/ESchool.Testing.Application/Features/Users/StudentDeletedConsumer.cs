using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion;
using ESchool.Testing.Domain;
using MassTransit;

namespace ESchool.Testing.Application.Features.Users
{
    public class StudentDeletedConsumer : IConsumer<StudentDeletedEvent>
    {
        private readonly Lazy<TestingContext> lazyDbContext;

        public StudentDeletedConsumer(Lazy<TestingContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<StudentDeletedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var student = await dbContext.Students.FindAsync(context.Message.Id);
            if (student != null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
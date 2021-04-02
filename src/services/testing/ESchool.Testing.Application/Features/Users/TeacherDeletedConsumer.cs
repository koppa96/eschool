using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion;
using ESchool.Testing.Domain;
using MassTransit;

namespace ESchool.Testing.Application.Features.Users
{
    public class TeacherDeletedConsumer : IConsumer<TeacherDeletedEvent>
    {
        private readonly Lazy<TestingContext> lazyDbContext;

        public TeacherDeletedConsumer(Lazy<TestingContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<TeacherDeletedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var teacher = await dbContext.Teachers.FindAsync(context.Message.Id);
            if (teacher != null)
            {
                dbContext.Teachers.Remove(teacher);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
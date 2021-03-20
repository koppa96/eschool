using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion;
using ESchool.HomeAssignments.Domain;
using MassTransit;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class TeacherDeletedConsumer : IConsumer<TeacherDeletedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;

        public TeacherDeletedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext)
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
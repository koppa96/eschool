using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.Lessons;
using ESchool.HomeAssignments.Domain;
using MassTransit;

namespace ESchool.HomeAssignments.Application.Features.Lessons
{
    public class LessonDeletedConsumer : IConsumer<LessonDeletedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;

        public LessonDeletedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<LessonDeletedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var lesson = await dbContext.Lessons.FindAsync(context.Message.LessonId);
            if (lesson != null)
            {
                dbContext.Lessons.Remove(lesson);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
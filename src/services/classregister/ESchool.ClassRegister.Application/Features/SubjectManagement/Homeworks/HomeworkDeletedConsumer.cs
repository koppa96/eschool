using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.HomeAssignments.Interface.IntegrationEvents;
using MassTransit;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Homeworks
{
    public class HomeworkDeletedConsumer : IConsumer<HomeworkDeletedEvent>
    {
        private readonly Lazy<ClassRegisterContext> lazyDbContext;

        public HomeworkDeletedConsumer(Lazy<ClassRegisterContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<HomeworkDeletedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var homework = await dbContext.HomeWorks.FindAsync(context.Message.Id);
            if (homework != null)
            {
                dbContext.HomeWorks.Remove(homework);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
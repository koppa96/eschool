using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.HomeAssignments.Interface.IntegrationEvents;
using MassTransit;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Homeworks
{
    public class HomeworkCreatedOrUpdatedConsumer : IConsumer<HomeworkCreatedOrUpdatedEvent>
    {
        private readonly Lazy<ClassRegisterContext> lazyDbContext;

        public HomeworkCreatedOrUpdatedConsumer(Lazy<ClassRegisterContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<HomeworkCreatedOrUpdatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            
            var existingHomework = await dbContext.HomeWorks.FindAsync(context.Message.Id);
            if (existingHomework == null)
            {
                existingHomework = new HomeWork
                {
                    Id = context.Message.Id,
                    LessonId = context.Message.LessonId
                };
                dbContext.HomeWorks.Add(existingHomework);
            }

            existingHomework.Title = context.Message.Title;
            await dbContext.SaveChangesAsync();
        }
    }
}
using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.IntegrationEvents.Lessons;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using MassTransit;

namespace ESchool.HomeAssignments.Application.Features.Lessons
{
    public class LessonCreatedOrUpdatedConsumer : IConsumer<LessonCreatedOrUpdatedEvent>
    {
        private readonly LessonService.LessonServiceClient client;
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;

        public LessonCreatedOrUpdatedConsumer(LessonService.LessonServiceClient client, Lazy<HomeAssignmentsContext> lazyDbContext)
        {
            this.client = client;
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<LessonCreatedOrUpdatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var lessonDetails = await client.GetLessonInfoForHomeAssignmentsAsync(
                new LessonInfoForHomeAssignmentsRequest
                {
                    Id = context.Message.LessonId.ToString()
                });

            var lesson = await dbContext.Lessons.FindAsync(context.Message.LessonId);
            if (lesson == null)
            {
                lesson = new Lesson
                {
                    Id = context.Message.LessonId,
                    ClassSchoolYearSubjectId = Guid.Parse(lessonDetails.ClassSchoolYearSubjectId)
                };
                dbContext.Lessons.Add(lesson);
            }

            lesson.Title = lessonDetails.Title;
            await dbContext.SaveChangesAsync();
        }
    }
}
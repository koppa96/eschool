using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.ClassRegister.Interface.IntegrationEvents.Lessons;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Outbox.Services;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonDeleteHandler : DeleteHandler<LessonDeleteCommand, Lesson>
    {
        private readonly ClassRegisterContext context;
        private readonly IEventPublisher eventPublisher;

        public LessonDeleteHandler(ClassRegisterContext context, IEventPublisher eventPublisher) : base(context)
        {
            this.context = context;
            this.eventPublisher = eventPublisher;
        }

        protected override Task ThrowIfCannotDeleteAsync(Lesson entity)
        {
            eventPublisher.Setup(context);
            return eventPublisher.PublishAsync(new LessonDeletedEvent
            {
                LessonId = entity.Id
            });
        }
    }
}
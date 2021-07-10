using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Users.Students;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users.Students
{
    public class RemoveStudentFromClassHandler : IRequestHandler<RemoveStudentFromClassCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IEventPublisher eventPublisher;

        public RemoveStudentFromClassHandler(ClassRegisterContext context, IEventPublisher eventPublisher)
        {
            this.context = context;
            this.eventPublisher = eventPublisher;
        }
        
        public async Task<Unit> Handle(RemoveStudentFromClassCommand request, CancellationToken cancellationToken)
        {
            var student = await context.Students.Include(x => x.Class)
                    .ThenInclude(x => x.ClassSchoolYears)
                        .ThenInclude(x => x.ClassSchoolYearSubjects)
                .SingleAsync(x => x.Id == request.StudentId, cancellationToken);

            var @class = student.Class;
            student.Class = null;
            
            foreach (var classSchoolYearSubject in @class.ClassSchoolYears.SelectMany(x => x.ClassSchoolYearSubjects))
            {
                await eventPublisher.PublishAsync(new ClassSchoolYearSubjectCreatedOrUpdatedEvent
                {
                    Id = classSchoolYearSubject.Id
                }, cancellationToken);
            }
            
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
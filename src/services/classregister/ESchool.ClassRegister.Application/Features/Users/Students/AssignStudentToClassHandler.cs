using System;
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
    public class AssignStudentToClassHandler : IRequestHandler<AssignStudentToClassCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IEventPublisher eventPublisher;

        public AssignStudentToClassHandler(ClassRegisterContext context, IEventPublisher eventPublisher)
        {
            this.context = context;
            this.eventPublisher = eventPublisher;
        }
        
        public async Task<Unit> Handle(AssignStudentToClassCommand request, CancellationToken cancellationToken)
        {
            var student = await context.Students.FindOrThrowAsync(request.StudentId, cancellationToken);
            if (student.ClassId != null)
            {
                throw new InvalidOperationException("Ez a diák már hozzá van rendelve egy osztályhoz.");
            }

            var @class = await context.Classes.Include(x => x.ClassSchoolYears)
                .ThenInclude(x => x.ClassSchoolYearSubjects)
                .SingleAsync(x => x.Id == request.ClassId, cancellationToken);
            
            student.Class = @class;
            
            eventPublisher.Setup(context);
            foreach (var classSchoolYearSubject in @class.ClassSchoolYears.SelectMany(x => x.ClassSchoolYearSubjects))
            {
                await eventPublisher.PublishAsync(new ClassSchoolYearSubjectCreatedOrUpdatedEvent
                {
                    ClassId = classSchoolYearSubject.ClassSchoolYear.ClassId,
                    SubjectId = classSchoolYearSubject.SubjectId,
                    SchoolYearId = classSchoolYearSubject.ClassSchoolYear.SchoolYearId
                }, cancellationToken);
            }
            
            await context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.Libs.Outbox.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectDeleteHandler : IRequestHandler<ClassSchoolYearSubjectDeleteCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IEventPublisher eventPublisher;

        public ClassSchoolYearSubjectDeleteHandler(ClassRegisterContext context,
            IEventPublisher eventPublisher)
        {
            this.context = context;
            this.eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(ClassSchoolYearSubjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var classSchoolYearSubject = await context.ClassSchoolYearSubjects.Include(x => x.Lessons)
                .Include(x => x.ClassSchoolYear)
                .SingleAsync(x => x.ClassSchoolYear.ClassId == request.ClassId &&
                                  x.ClassSchoolYear.SchoolYearId == request.SchoolYearId &&
                                  x.SubjectId == request.SubjectId, cancellationToken);

            if (classSchoolYearSubject.Lessons.Any())
            {
                throw new InvalidOperationException(
                    "Nem távolítható el olyan tárgy, amelyhez már vannak órák felvéve.");
            }

            eventPublisher.Setup(context);
            await eventPublisher.PublishAsync(new ClassSchoolYearSubjectDeletedEvent
            {
                ClassId = classSchoolYearSubject.ClassSchoolYear.ClassId,
                SchoolYearId = classSchoolYearSubject.ClassSchoolYear.SchoolYearId,
                SubjectId = classSchoolYearSubject.SubjectId
            }, cancellationToken);
            context.ClassSchoolYearSubjects.Remove(classSchoolYearSubject);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
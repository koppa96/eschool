using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.Libs.Outbox.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectCreateHandler : IRequestHandler<ClassSchoolYearSubjectCreateCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IEventPublisher eventPublisher;

        public ClassSchoolYearSubjectCreateHandler(ClassRegisterContext context, IEventPublisher eventPublisher)
        {
            this.context = context;
            this.eventPublisher = eventPublisher;
        }
        
        public async Task<Unit> Handle(ClassSchoolYearSubjectCreateCommand request, CancellationToken cancellationToken)
        {
            var classSchoolYear = await context.ClassSchoolYears.SingleAsync(
                x => x.ClassId == request.ClassId && x.SchoolYearId == request.SchoolYearId, cancellationToken);

            if (await context.ClassSchoolYearSubjects.AnyAsync(x =>
                x.SubjectId == request.SubjectId && x.ClassSchoolYearId == classSchoolYear.Id, cancellationToken))
            {
                throw new InvalidOperationException(
                    "Ez a tárgy már hozzá lett rendelve az adott osztályhoz az adott tanévre.");
            }

            context.ClassSchoolYearSubjects.Add(new ClassSchoolYearSubject
            {
                SubjectId = request.SubjectId,
                ClassSchoolYearId = classSchoolYear.Id,
                ClassSchoolYearSubjectTeachers = request.TeacherIds.Select(x => new ClassSchoolYearSubjectTeacher
                {
                    TeacherId = x
                }).ToList()
            });
            
            eventPublisher.Setup(context);
            await eventPublisher.PublishAsync(new ClassSchoolYearSubjectCreatedOrUpdatedEvent
            {
                ClassId = classSchoolYear.ClassId,
                SchoolYearId = classSchoolYear.SchoolYearId,
                SubjectId = request.SubjectId
            }, cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
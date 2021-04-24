using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users.Teachers
{
    public class RemoveTeacherFromClassSchoolYearSubjectCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid TeacherId { get; set; }
    }

    public class RemoveTeacherFromClassSchoolYearSubjectHandler : IRequestHandler<RemoveTeacherFromClassSchoolYearSubjectCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IEventPublisher eventPublisher;

        public RemoveTeacherFromClassSchoolYearSubjectHandler(ClassRegisterContext context,
            IEventPublisher eventPublisher)
        {
            this.context = context;
            this.eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(RemoveTeacherFromClassSchoolYearSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var classSchoolYearSubject = await context.ClassSchoolYearSubjects
                .Include(x => x.ClassSchoolYearSubjectTeachers)
                .SingleAsync(x =>
                    x.ClassSchoolYear.ClassId == request.ClassId &&
                    x.ClassSchoolYear.SchoolYearId == request.SchoolYearId &&
                    x.SubjectId == request.SubjectId, cancellationToken);

            var classSchoolYearSubjectTeacher =
                classSchoolYearSubject.ClassSchoolYearSubjectTeachers.SingleOrDefault(x => x.TeacherId == request.TeacherId);

            if (classSchoolYearSubjectTeacher != null)
            {
                context.ClassSchoolYearSubjectTeachers.Remove(classSchoolYearSubjectTeacher);
                
                await eventPublisher.PublishAsync(new ClassSchoolYearSubjectCreatedOrUpdatedEvent
                {
                    Id = classSchoolYearSubject.Id
                }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
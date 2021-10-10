using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Users.Teachers;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users.Teachers
{
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
                .Include(x => x.ClassSchoolYear)
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
                
                eventPublisher.Setup(context);
                await eventPublisher.PublishAsync(new ClassSchoolYearSubjectCreatedOrUpdatedEvent
                {
                    ClassId = classSchoolYearSubject.ClassSchoolYear.ClassId,
                    SchoolYearId = classSchoolYearSubject.ClassSchoolYear.SchoolYearId,
                    SubjectId = classSchoolYearSubject.SubjectId
                }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
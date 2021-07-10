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
    public class ClassSchoolYearSubjectEditHandler : IRequestHandler<ClassSchoolYearSubjectEditCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IEventPublisher eventPublisher;

        public ClassSchoolYearSubjectEditHandler(ClassRegisterContext context, IEventPublisher eventPublisher)
        {
            this.context = context;
            this.eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(ClassSchoolYearSubjectEditCommand request, CancellationToken cancellationToken)
        {
            var classSchoolYearSubject = await context.ClassSchoolYearSubjects
                .Include(x => x.ClassSchoolYearSubjectTeachers)
                .SingleAsync(x =>
                    x.ClassSchoolYear.ClassId == request.ClassId &&
                    x.ClassSchoolYear.SchoolYearId == request.SchoolYearId &&
                    x.SubjectId == request.SubjectId, cancellationToken);

            context.ClassSchoolYearSubjectTeachers.RemoveRange(classSchoolYearSubject.ClassSchoolYearSubjectTeachers);
            context.ClassSchoolYearSubjectTeachers.AddRange(request.TeacherIds.Select(x => new ClassSchoolYearSubjectTeacher
            {
                TeacherId = x,
                ClassSchoolYearSubjectId = classSchoolYearSubject.Id
            }));

            await eventPublisher.PublishAsync(new ClassSchoolYearSubjectCreatedOrUpdatedEvent
            {
                Id = classSchoolYearSubject.Id
            }, cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
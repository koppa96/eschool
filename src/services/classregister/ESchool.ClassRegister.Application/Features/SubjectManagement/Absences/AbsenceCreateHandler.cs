using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Domain.Enums;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class AbsenceCreateCommand : IRequest
    {
        public Guid LessonId { get; set; }
        public Guid StudentId { get; set; }
    }
    
    public class AbsenceCreateHandler : IRequestHandler<AbsenceCreateCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public AbsenceCreateHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<Unit> Handle(AbsenceCreateCommand request, CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.ClassSchoolYear)
                        .ThenInclude(x => x.Class)
                            .ThenInclude(x => x.Students)
                .Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                .SingleAsync(x => x.Id == request.LessonId, cancellationToken);

            var currentUserId = identityService.GetCurrentUserId();
            if (lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.All(x => x.TeacherId != currentUserId))
            {
                throw new UnauthorizedAccessException(
                    "Csak olyan tanárok vehetnek fel hiányzást, akik tanítják az adott tárgyat, az osztálynak a tanévben.");
            }

            // Ensure that the student is in the class that is supposed to be on this lesson
            var student = lesson.ClassSchoolYearSubject.ClassSchoolYear.Class.Students.Single(x => x.Id == request.StudentId);
            context.Absences.Add(new Absence
            {
                Lesson = lesson,
                Student = student,
                AbsenceState = AbsenceState.Unverified
            });

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
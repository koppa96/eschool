using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectDeleteHandler : IRequestHandler<ClassSchoolYearSubjectDeleteCommand>
    {
        private readonly ClassRegisterContext context;

        public ClassSchoolYearSubjectDeleteHandler(ClassRegisterContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(ClassSchoolYearSubjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var classSchoolYearSubject = await context.ClassSchoolYearSubjects.Include(x => x.Lessons)
                .SingleAsync(x => x.ClassSchoolYear.ClassId == request.ClassId &&
                                  x.ClassSchoolYear.SchoolYearId == request.SchoolYearId &&
                                  x.SubjectId == request.SubjectId, cancellationToken);

            if (classSchoolYearSubject.Lessons.Any())
            {
                throw new InvalidOperationException(
                    "Nem távolítható el olyan tárgy, amelyhez már vannak órák felvéve.");
            }

            context.ClassSchoolYearSubjects.Remove(classSchoolYearSubject);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
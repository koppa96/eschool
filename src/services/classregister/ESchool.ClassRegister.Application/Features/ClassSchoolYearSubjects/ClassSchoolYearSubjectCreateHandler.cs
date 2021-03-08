using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectCreateCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
        public List<Guid> TeacherIds { get; set; }
    }
    
    public class ClassSchoolYearSubjectCreateHandler : IRequestHandler<ClassSchoolYearSubjectCreateCommand>
    {
        private readonly ClassRegisterContext context;

        public ClassSchoolYearSubjectCreateHandler(ClassRegisterContext context)
        {
            this.context = context;
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

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
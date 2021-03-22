﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectEditCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
        public List<Guid> TeacherIds { get; set; }
    }

    public class ClassSchoolYearSubjectEditHandler : IRequestHandler<ClassSchoolYearSubjectEditCommand>
    {
        private readonly ClassRegisterContext context;

        public ClassSchoolYearSubjectEditHandler(ClassRegisterContext context)
        {
            this.context = context;
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

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
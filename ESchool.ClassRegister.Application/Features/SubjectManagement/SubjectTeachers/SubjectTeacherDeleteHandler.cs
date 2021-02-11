﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.SubjectTeachers
{
    public class SubjectTeacherDeleteCommand : IRequest
    {
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }
    
    public class SubjectTeacherDeleteHandler : IRequestHandler<SubjectTeacherDeleteCommand>
    {
        private readonly ClassRegisterContext context;

        public SubjectTeacherDeleteHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(SubjectTeacherDeleteCommand request, CancellationToken cancellationToken)
        {
            var subjectTeacher = await context.SubjectTeachers.SingleOrDefaultAsync(x =>
                x.SubjectId == request.SubjectId && x.TeacherId == request.TeacherId, cancellationToken);

            if (subjectTeacher != null)
            {
                context.SubjectTeachers.Remove(subjectTeacher);
                await context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}
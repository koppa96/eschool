using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users.Students
{
    public class AssignStudentToClassCommand : IRequest
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
    }
    
    public class AssignStudentToClassHandler : IRequestHandler<AssignStudentToClassCommand>
    {
        private readonly ClassRegisterContext context;

        public AssignStudentToClassHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(AssignStudentToClassCommand request, CancellationToken cancellationToken)
        {
            var student = await context.Students.FindOrThrowAsync(request.StudentId, cancellationToken);
            if (student.ClassId != null)
            {
                throw new InvalidOperationException("Ez a diák már hozzá van rendelve egy osztályhoz.");
            }

            student.ClassId = request.ClassId;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Domain.Extensions;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Users.Students
{
    public class RemoveStudentFromClassCommand : IRequest
    {
        public Guid StudentId { get; set; }
    }
    
    public class RemoveStudentFromClassHandler : IRequestHandler<RemoveStudentFromClassCommand>
    {
        private readonly ClassRegisterContext context;

        public RemoveStudentFromClassHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(RemoveStudentFromClassCommand request, CancellationToken cancellationToken)
        {
            var student = await context.Students.FindOrThrowAsync(request.StudentId, cancellationToken);
            student.ClassId = null;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
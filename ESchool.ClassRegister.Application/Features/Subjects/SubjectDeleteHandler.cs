using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    
    public class SubjectDeleteHandler : IRequestHandler<SubjectDeleteCommand>
    {
        private readonly ClassRegisterContext context;

        public SubjectDeleteHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(SubjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var subject = await context.Subjects.FindAsync(request.Id, cancellationToken);
            context.Subjects.Remove(subject);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
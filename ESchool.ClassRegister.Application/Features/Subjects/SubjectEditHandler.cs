using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Subjects.Common;
using ESchool.ClassRegister.Domain;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectEditCommand : IRequest<SubjectDetailsResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    
    public class SubjectEditHandler : IRequestHandler<SubjectEditCommand, SubjectDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public SubjectEditHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<SubjectDetailsResponse> Handle(SubjectEditCommand request, CancellationToken cancellationToken)
        {
            var subject = await context.Subjects.FindAsync(request.Id, cancellationToken);
            subject.Name = request.Name;

            await context.SaveChangesAsync(cancellationToken);
            return new SubjectDetailsResponse
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }
    }
}
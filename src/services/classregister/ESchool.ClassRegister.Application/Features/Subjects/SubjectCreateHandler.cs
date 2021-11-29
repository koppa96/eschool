using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Subjects.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Subjects;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectCreateHandler : IRequestHandler<SubjectCreateCommand, SubjectDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public SubjectCreateHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<SubjectDetailsResponse> Handle(SubjectCreateCommand request, CancellationToken cancellationToken)
        {
            var subject = new Subject
            {
                Name = request.Name
            };

            context.Subjects.Add(subject);
            await context.SaveChangesAsync(cancellationToken);

            return new SubjectDetailsResponse
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }
    }
}
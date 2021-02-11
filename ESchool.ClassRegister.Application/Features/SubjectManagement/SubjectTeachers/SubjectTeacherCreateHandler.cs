using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.SubjectTeachers
{
    public class SubjectTeacherCreateCommand : IRequest
    {
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }

    public class SubjectTeacherCreateHandler : IRequestHandler<SubjectTeacherCreateCommand>
    {
        private readonly ClassRegisterContext context;

        public SubjectTeacherCreateHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(SubjectTeacherCreateCommand request, CancellationToken cancellationToken)
        {
            var subjectTeacher = new SubjectTeacher
            {
                SubjectId = request.SubjectId,
                TeacherId = request.TeacherId
            };

            context.SubjectTeachers.Add(subjectTeacher);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
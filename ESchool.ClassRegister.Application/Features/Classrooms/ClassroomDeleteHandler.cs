using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class ClassroomDeleteHandler : IRequestHandler<ClassroomDeleteCommand>
    {
        private readonly ClassRegisterContext context;

        public ClassroomDeleteHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(ClassroomDeleteCommand request, CancellationToken cancellationToken)
        {
            var classroom = await context.ClassRooms.FindAsync(request.Id, cancellationToken);
            context.ClassRooms.Remove(classroom);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
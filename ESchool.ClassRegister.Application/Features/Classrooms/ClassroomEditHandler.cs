using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Classrooms.Common;
using ESchool.ClassRegister.Domain;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomEditCommand : IRequest<ClassroomDetailsResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    
    public class ClassroomEditHandler : IRequestHandler<ClassroomEditCommand, ClassroomDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public ClassroomEditHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<ClassroomDetailsResponse> Handle(ClassroomEditCommand request, CancellationToken cancellationToken)
        {
            var classroom = await context.ClassRooms.FindAsync(request.Id, cancellationToken);
            classroom.Name = request.Name;

            await context.SaveChangesAsync(cancellationToken);
            return new ClassroomDetailsResponse
            {
                Id = classroom.Id,
                Name = classroom.Name
            };
        }
    }
}
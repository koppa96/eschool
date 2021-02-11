using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Classrooms.Common;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomEditCommand
    {
        public string Name { get; set; }
    }
    
    public class ClassroomEditHandler : IRequestHandler<EditCommand<ClassroomEditCommand, ClassroomDetailsResponse>, ClassroomDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public ClassroomEditHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<ClassroomDetailsResponse> Handle(EditCommand<ClassroomEditCommand, ClassroomDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var classroom = await context.ClassRooms.FindAsync(request.Id, cancellationToken);
            classroom.Name = request.InnerCommand.Name;

            await context.SaveChangesAsync(cancellationToken);
            return new ClassroomDetailsResponse
            {
                Id = classroom.Id,
                Name = classroom.Name
            };
        }
    }
}
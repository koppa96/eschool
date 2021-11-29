using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Classrooms;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Interface.Commands;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
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
            var classroom = await context.Classrooms.FindOrThrowAsync(request.Id, cancellationToken);
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
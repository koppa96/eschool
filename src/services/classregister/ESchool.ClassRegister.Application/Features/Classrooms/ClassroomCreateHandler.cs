using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Classrooms;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomCreateHandler : IRequestHandler<ClassroomCreateCommand, ClassroomDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public ClassroomCreateHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<ClassroomDetailsResponse> Handle(ClassroomCreateCommand request, CancellationToken cancellationToken)
        {
            var classroom = new Classroom
            {
                Name = request.Name
            };
            
            context.Classrooms.Add(classroom);
            await context.SaveChangesAsync(cancellationToken);
            
            return new ClassroomDetailsResponse
            {
                Id = classroom.Id,
                Name = classroom.Name
            };
        }
    }
}
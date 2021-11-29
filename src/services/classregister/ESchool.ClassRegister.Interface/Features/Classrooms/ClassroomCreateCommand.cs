using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Classrooms
{
    public class ClassroomCreateCommand : IRequest<ClassroomDetailsResponse>
    {
        public string Name { get; set; }
    }
}
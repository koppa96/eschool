using MediatR;

namespace ESchool.ClassRegister.Interface.Features.ClassTypes
{
    public class ClassTypeEditCommand : IRequest<ClassTypeDetailsResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartingGrade { get; set; }
    }
}
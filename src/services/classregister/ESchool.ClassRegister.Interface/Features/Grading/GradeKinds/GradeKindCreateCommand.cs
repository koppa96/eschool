using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Grading.GradeKinds
{
    public class GradeKindCreateCommand : IRequest<GradeKindResponse>
    {
        public string Name { get; set; }
        public double AverageMultiplier { get; set; }
    }
}
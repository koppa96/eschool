using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Subjects
{
    public class SubjectCreateCommand : IRequest<SubjectDetailsResponse>
    {
        public string Name { get; set; }
    }
}
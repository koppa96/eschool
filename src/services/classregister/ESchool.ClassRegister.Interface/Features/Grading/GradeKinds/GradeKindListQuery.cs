using System.Collections.Generic;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Grading.GradeKinds
{
    public class GradeKindListQuery : IRequest<List<GradeKindResponse>>
    {
    }
}
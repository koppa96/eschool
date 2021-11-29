using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Grading.GradeKinds
{
    public class GradeKindCreateHandler : IRequestHandler<GradeKindCreateCommand, GradeKindResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public GradeKindCreateHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<GradeKindResponse> Handle(GradeKindCreateCommand request, CancellationToken cancellationToken)
        {
            var gradeKind = new GradeKind
            {
                Name = request.Name,
                AverageMultiplier = request.AverageMultiplier
            };

            context.GradeKinds.Add(gradeKind);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<GradeKindResponse>(gradeKind);
        }
    }
}
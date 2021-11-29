using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Interface.Commands;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Grading.GradeKinds
{
    public class GradeKindEditHandler : IRequestHandler<EditCommand<GradeKindEditCommand, GradeKindResponse>, GradeKindResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public GradeKindEditHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<GradeKindResponse> Handle(EditCommand<GradeKindEditCommand, GradeKindResponse> request, CancellationToken cancellationToken)
        {
            var gradeKind = await context.GradeKinds.FindOrThrowAsync(request.Id, cancellationToken);

            gradeKind.Name = request.InnerCommand.Name;
            gradeKind.AverageMultiplier = request.InnerCommand.AverageMultiplier;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<GradeKindResponse>(gradeKind);
        }
    }
}
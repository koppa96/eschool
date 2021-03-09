using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Extensions;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Grading.GradeKinds
{
    public class GradeKindEditCommand
    {
        public string Name { get; set; }
        public double AverageMultiplier { get; set; }
    }
    
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
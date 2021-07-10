using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Interface.Commands;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeEditHandler : IRequestHandler<EditCommand<ClassTypeEditCommand, ClassTypeDetailsResponse>, ClassTypeDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public ClassTypeEditHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<ClassTypeDetailsResponse> Handle(EditCommand<ClassTypeEditCommand, ClassTypeDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var classType = await context.ClassTypes.FindOrThrowAsync(request.Id, cancellationToken);

            classType.Name = request.InnerCommand.Name;
            classType.Description = request.InnerCommand.Description;
            classType.StartingGrade = request.InnerCommand.StartingGrade;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<ClassTypeDetailsResponse>(classType);
        }
    }
}
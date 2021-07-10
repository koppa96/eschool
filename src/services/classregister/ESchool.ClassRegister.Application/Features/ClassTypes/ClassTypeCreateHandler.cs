using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeCreateHandler : IRequestHandler<ClassTypeCreateCommand, ClassTypeDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public ClassTypeCreateHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<ClassTypeDetailsResponse> Handle(ClassTypeCreateCommand request, CancellationToken cancellationToken)
        {
            var classType = new ClassType
            {
                Name = request.Name,
                Description = request.Description,
                StartingGrade = request.StartingGrade
            };

            context.ClassTypes.Add(classType);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<ClassTypeDetailsResponse>(classType);
        }
    }
}
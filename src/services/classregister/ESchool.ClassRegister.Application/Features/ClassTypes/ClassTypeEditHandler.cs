using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeEditCommand : IRequest<ClassTypeDetailsResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartingGrade { get; set; }
    }
    
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
            var classType = await context.ClassTypes.FindAsync(request.Id, cancellationToken);

            classType.Name = request.InnerCommand.Name;
            classType.Description = request.InnerCommand.Description;
            classType.StartingGrade = request.InnerCommand.StartingGrade;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<ClassTypeDetailsResponse>(classType);
        }
    }
}
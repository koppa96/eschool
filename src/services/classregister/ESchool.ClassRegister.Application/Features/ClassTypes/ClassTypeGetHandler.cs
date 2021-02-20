using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;
using ESchool.ClassRegister.Domain;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeGetQuery : IRequest<ClassTypeDetailsResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class ClassTypeGetHandler : IRequestHandler<ClassTypeGetQuery, ClassTypeDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public ClassTypeGetHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<ClassTypeDetailsResponse> Handle(ClassTypeGetQuery request, CancellationToken cancellationToken)
        {
            var classType = await context.ClassTypes.FindAsync(request.Id, cancellationToken);
            return mapper.Map<ClassTypeDetailsResponse>(classType);
        }
    }
}
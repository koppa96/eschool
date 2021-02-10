using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    
    public class SchoolYearDeleteHandler : IRequestHandler<SchoolYearDeleteCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public SchoolYearDeleteHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<Unit> Handle(SchoolYearDeleteCommand request, CancellationToken cancellationToken)
        {
            var schoolYear = await context.SchoolYears.FindAsync(request.Id, cancellationToken);
            context.SchoolYears.Remove(schoolYear);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
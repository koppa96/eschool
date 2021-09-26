using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassFinishCommand : IRequest<ClassDetailsResponse>
    {
        public Guid ClassId { get; set; }
    }

    public class ClassFinishHandler : IRequestHandler<ClassFinishCommand, ClassDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public ClassFinishHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<ClassDetailsResponse> Handle(ClassFinishCommand request, CancellationToken cancellationToken)
        {
            var @class = await context.Classes.Include(x => x.ClassType)
                .Include(x => x.HeadTeacher)
                .Include(x => x.ClassSchoolYears)
                .ThenInclude(x => x.SchoolYear)
                .SingleAsync(x => x.Id == request.ClassId, cancellationToken);

            @class.DidFinish = true;
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<ClassDetailsResponse>(@class);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.SchoolYears.Common;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearGetQuery : IRequest<SchoolYearDetailsResponse>
    {
        public Guid Id { get; set; }
    }

    public class SchoolYearGetHandler : IRequestHandler<SchoolYearGetQuery, SchoolYearDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public SchoolYearGetHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SchoolYearDetailsResponse> Handle(SchoolYearGetQuery request,
            CancellationToken cancellationToken)
        {
            var schoolYear = await context.SchoolYears.Include(x => x.ClassSchoolYears)
                    .ThenInclude(x => x.Class)
                        .ThenInclude(x => x.ClassType)
                .Include(x => x.ClassSchoolYears)
                    .ThenInclude(x => x.Class)
                        .ThenInclude(x => x.ClassSchoolYears)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<SchoolYearDetailsResponse>(schoolYear);
        }
    }
}
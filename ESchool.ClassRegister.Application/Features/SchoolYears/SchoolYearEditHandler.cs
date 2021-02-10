using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.SchoolYears.Common;
using ESchool.ClassRegister.Domain;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearEditCommand : IRequest<SchoolYearDetailsResponse>
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
    }
    
    public class SchoolYearEditHandler : IRequestHandler<SchoolYearEditCommand, SchoolYearDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public SchoolYearEditHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SchoolYearDetailsResponse> Handle(SchoolYearEditCommand request, CancellationToken cancellationToken)
        {
            var schoolYear = await context.SchoolYears.FindAsync(request.Id, cancellationToken);
            schoolYear.DisplayName = request.DisplayName;
            schoolYear.StartsAt = request.StartsAt;
            schoolYear.EndOfFirstHalf = request.EndOfFirstHalf;
            schoolYear.EndsAt = request.EndsAt;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<SchoolYearDetailsResponse>(schoolYear);
        }
    }
}
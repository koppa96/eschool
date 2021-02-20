using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Classes.Common;
using ESchool.ClassRegister.Application.Features.SchoolYears.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearCreateCommand : IRequest<SchoolYearDetailsResponse>
    {
        public string DisplayName { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
    }
    
    public class SchoolYearCreateHandler : IRequestHandler<SchoolYearCreateCommand, SchoolYearDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public SchoolYearCreateHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<SchoolYearDetailsResponse> Handle(SchoolYearCreateCommand request, CancellationToken cancellationToken)
        {
            var schoolYear = new SchoolYear
            {
                DisplayName = request.DisplayName,
                StartsAt = request.StartsAt,
                EndOfFirstHalf = request.EndOfFirstHalf,
                EndsAt = request.EndsAt
            };

            context.SchoolYears.Add(schoolYear);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<SchoolYearDetailsResponse>(schoolYear);
        }
    }
}
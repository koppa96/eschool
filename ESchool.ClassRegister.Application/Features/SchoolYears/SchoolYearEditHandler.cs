using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.SchoolYears.Common;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearEditCommand
    {
        public string DisplayName { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndOfFirstHalf { get; set; }
        public DateTime EndsAt { get; set; }
    }
    
    public class SchoolYearEditHandler : IRequestHandler<EditCommand<SchoolYearEditCommand, SchoolYearDetailsResponse>, SchoolYearDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public SchoolYearEditHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SchoolYearDetailsResponse> Handle(EditCommand<SchoolYearEditCommand, SchoolYearDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var schoolYear = await context.SchoolYears.FindAsync(request.Id, cancellationToken);
            schoolYear.DisplayName = request.InnerCommand.DisplayName;
            schoolYear.StartsAt = request.InnerCommand.StartsAt;
            schoolYear.EndOfFirstHalf = request.InnerCommand.EndOfFirstHalf;
            schoolYear.EndsAt = request.InnerCommand.EndsAt;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<SchoolYearDetailsResponse>(schoolYear);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkEditHandler : IRequestHandler<EditCommand<HomeworkEditCommand, HomeworkDetailsResponse>, HomeworkDetailsResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IMapper mapper;

        public HomeworkEditHandler(HomeAssignmentsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkDetailsResponse> Handle(EditCommand<HomeworkEditCommand, HomeworkDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var homework = await context.Homeworks.Include(x => x.CreatedBy)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            
            homework.Title = request.InnerCommand.Title;
            homework.Description = request.InnerCommand.Description;
            homework.Optional = request.InnerCommand.Optional;
            
            var localDate = request.InnerCommand.Deadline.ToLocalTime();
            homework.Deadline = homework.Deadline <= localDate
                ? localDate
                : throw new InvalidOperationException("A határidő nem módosítható az eredetinél korábbi időpontra.");

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkDetailsResponse>(homework);
        }
    }
}
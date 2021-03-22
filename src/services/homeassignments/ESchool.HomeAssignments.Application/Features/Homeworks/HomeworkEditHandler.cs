using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.Homeworks.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using MediatR;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkEditCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Optional { get; set; }
        public DateTime Deadline { get; set; }
    }
    
    public class HomeworkEditHandler : IRequestHandler<EditCommand<HomeworkEditCommand, HomeworkDetailsResponse>, HomeworkDetailsResponse>
    {
        private readonly HomeAssignmentsContext context;

        public HomeworkEditHandler(HomeAssignmentsContext context)
        {
            this.context = context;
        }
        
        public Task<HomeworkDetailsResponse> Handle(EditCommand<HomeworkEditCommand, HomeworkDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkDeleteCommand : DeleteCommand
    {
    }
    
    public class HomeworkDeleteHandler : DeleteHandler<HomeworkDeleteCommand, Homework>
    {
        public HomeworkDeleteHandler(HomeAssignmentsContext context) : base(context)
        {
        }
    }
}
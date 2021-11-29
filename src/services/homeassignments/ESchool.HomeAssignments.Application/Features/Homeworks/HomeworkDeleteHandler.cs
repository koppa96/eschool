using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkDeleteHandler : DeleteHandler<HomeworkDeleteCommand, Homework>
    {
        public HomeworkDeleteHandler(HomeAssignmentsContext context) : base(context)
        {
        }
    }
}
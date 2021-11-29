using System;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.Homeworks
{
    public class HomeworkGetQuery : IRequest<HomeworkDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}
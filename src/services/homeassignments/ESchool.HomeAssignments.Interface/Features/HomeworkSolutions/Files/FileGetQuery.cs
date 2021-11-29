using System;
using MediatR;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions.Files
{
    public class FileGetQuery : IRequest<FileGetResponse>
    {
        public Guid FileId { get; set; }
    }
}
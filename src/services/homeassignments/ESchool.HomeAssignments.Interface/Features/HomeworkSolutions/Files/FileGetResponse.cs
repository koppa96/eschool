using System.IO;

namespace ESchool.HomeAssignments.Interface.Features.HomeworkSolutions.Files
{
    public class FileGetResponse
    {
        public string Name { get; set; }
        public Stream Stream { get; set; }
    }
}
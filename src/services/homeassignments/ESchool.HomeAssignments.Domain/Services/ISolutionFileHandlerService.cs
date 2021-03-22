using System;
using System.IO;
using System.Threading.Tasks;

namespace ESchool.HomeAssignments.Domain.Services
{
    public interface ISolutionFileHandlerService
    {
        Task SaveAsync(Guid solutionId, string fileName, Stream stream);
        Task<Stream> OpenAsync(Guid solutionId, string fileName);
        Task DeleteAsync(Guid solutionId, string fileName);
    }
}
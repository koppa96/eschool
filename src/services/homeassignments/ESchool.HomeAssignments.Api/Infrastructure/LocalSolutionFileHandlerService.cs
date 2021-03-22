using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Api.Infrastructure.Configuration;
using ESchool.HomeAssignments.Domain.Services;
using Microsoft.Extensions.Options;

namespace ESchool.HomeAssignments.Api.Infrastructure
{
    public class LocalSolutionFileHandlerService : ISolutionFileHandlerService
    {
        private readonly LocalSolutionFileHandlerConfig config;

        public LocalSolutionFileHandlerService(IOptions<LocalSolutionFileHandlerConfig> options)
        {
            config = options.Value;
        }
        
        public async Task SaveAsync(Guid solutionId, string fileName, Stream stream)
        {
            var directoryPath = Path.Combine(config.BasePath, solutionId.ToString());
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, fileName);
            await using var fileStream = File.OpenWrite(filePath);
            await stream.CopyToAsync(fileStream);
        }

        public Task<Stream> OpenAsync(Guid solutionId, string fileName)
        {
            var filePath = Path.Combine(config.BasePath, solutionId.ToString(), fileName);
            return Task.FromResult<Stream>(File.OpenRead(filePath));
        }

        public Task DeleteAsync(Guid solutionId, string fileName)
        {
            var filePath = Path.Combine(config.BasePath, solutionId.ToString(), fileName);
            File.Delete(filePath);

            var directoryPath = Path.Combine(config.BasePath, solutionId.ToString());
            if (!Directory.EnumerateFiles(directoryPath).Any())
            {
                Directory.Delete(directoryPath);
            }

            return Task.CompletedTask;
        }
    }
}
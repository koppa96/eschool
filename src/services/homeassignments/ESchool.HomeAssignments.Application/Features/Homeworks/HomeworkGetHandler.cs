using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkGetHandler : IRequestHandler<HomeworkGetQuery, HomeworkDetailsResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IConfigurationProvider configurationProvider;

        public HomeworkGetHandler(HomeAssignmentsContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<HomeworkDetailsResponse> Handle(HomeworkGetQuery request, CancellationToken cancellationToken)
        {
            return context.Homeworks.Where(x => x.Id == request.Id)
                .ProjectTo<HomeworkDetailsResponse>(configurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}
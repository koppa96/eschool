using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassGetHandler : IRequestHandler<ClassGetQuery, ClassDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public ClassGetHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<ClassDetailsResponse> Handle(ClassGetQuery request, CancellationToken cancellationToken)
        {
            return context.Classes.Where(x => x.Id == request.Id)
                .ProjectTo<ClassDetailsResponse>(configurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}
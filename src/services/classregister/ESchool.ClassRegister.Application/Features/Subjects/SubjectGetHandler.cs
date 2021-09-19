using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Subjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectGetHandler : IRequestHandler<SubjectGetQuery, SubjectDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public SubjectGetHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<SubjectDetailsResponse> Handle(SubjectGetQuery request, CancellationToken cancellationToken)
        {
            return context.Subjects.ProjectTo<SubjectDetailsResponse>(configurationProvider)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
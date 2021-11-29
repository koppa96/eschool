using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeGetQuery : IRequest<GradeDetailsResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class GradeGetHandler : IRequestHandler<GradeGetQuery, GradeDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public GradeGetHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<GradeDetailsResponse> Handle(GradeGetQuery request, CancellationToken cancellationToken)
        {
            return context.Grades.Where(x => x.Id == request.Id)
                .ProjectTo<GradeDetailsResponse>(configurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}
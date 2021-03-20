using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonGetCommand : IRequest<LessonDetailsResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class LessonGetHandler : IRequestHandler<LessonGetCommand, LessonDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public LessonGetHandler(ClassRegisterContext context,
            IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<LessonDetailsResponse> Handle(LessonGetCommand request, CancellationToken cancellationToken)
        {
            return context.Lessons.Where(x => x.Id == request.Id)
                .ProjectTo<LessonDetailsResponse>(configurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}
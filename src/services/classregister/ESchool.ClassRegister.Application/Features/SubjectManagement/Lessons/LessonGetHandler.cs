﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonGetHandler : IRequestHandler<LessonGetQuery, LessonDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public LessonGetHandler(ClassRegisterContext context,
            IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<LessonDetailsResponse> Handle(LessonGetQuery request, CancellationToken cancellationToken)
        {
            return context.Lessons.Where(x => x.Id == request.Id)
                .ProjectTo<LessonDetailsResponse>(configurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}
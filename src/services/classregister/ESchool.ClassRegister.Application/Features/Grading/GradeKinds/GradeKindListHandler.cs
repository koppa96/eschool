using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.GradeKinds
{
    public class GradeKindListQuery : IRequest<List<GradeKindResponse>>
    {
    }

    public class GradeKindListHandler : IRequestHandler<GradeKindListQuery, List<GradeKindResponse>>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider provider;

        public GradeKindListHandler(ClassRegisterContext context, IConfigurationProvider provider)
        {
            this.context = context;
            this.provider = provider;
        }
        
        public Task<List<GradeKindResponse>> Handle(GradeKindListQuery request, CancellationToken cancellationToken)
        {
            return context.GradeKinds.ProjectTo<GradeKindResponse>(provider)
                .ToListAsync(cancellationToken);
        }
    }
}
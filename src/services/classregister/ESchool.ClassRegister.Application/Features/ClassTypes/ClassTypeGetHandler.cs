﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.Libs.Domain.Extensions;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeGetHandler : IRequestHandler<ClassTypeGetQuery, ClassTypeDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public ClassTypeGetHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<ClassTypeDetailsResponse> Handle(ClassTypeGetQuery request, CancellationToken cancellationToken)
        {
            var classType = await context.ClassTypes.FindOrThrowAsync(request.Id, cancellationToken);
            return mapper.Map<ClassTypeDetailsResponse>(classType);
        }
    }
}
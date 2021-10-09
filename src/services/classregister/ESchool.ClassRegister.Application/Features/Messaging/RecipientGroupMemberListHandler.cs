using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Application.Features.Users;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class RecipientGroupMemberListQuery : IRequest<List<ClassRegisterUserListResponse>>
    {
        public Guid Id { get; set; }
    }

    public class RecipientGroupMemberListHandler : IRequestHandler<RecipientGroupMemberListQuery, List<ClassRegisterUserListResponse>>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public RecipientGroupMemberListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<List<ClassRegisterUserListResponse>> Handle(RecipientGroupMemberListQuery request, CancellationToken cancellationToken)
        {
            return context.RecipientGroupMembers.Where(x => x.RecipientGroupId == request.Id)
                .Select(x => x.Member)
                .ProjectTo<ClassRegisterUserListResponse>(configurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
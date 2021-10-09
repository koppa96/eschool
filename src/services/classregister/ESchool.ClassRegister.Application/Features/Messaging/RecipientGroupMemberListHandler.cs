using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Application.Features.Users;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Domain.Enums;
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
        
        public async Task<List<ClassRegisterUserListResponse>> Handle(RecipientGroupMemberListQuery request, CancellationToken cancellationToken)
        {
            var members = await context.RecipientGroupMembers.Where(x => x.RecipientGroupId == request.Id)
                .Select(x => new ClassRegisterUserListResponse
                {
                    Id = x.MemberId,
                    Name = x.Member.Name,
                    Roles = x.Member.UserRoles.Select(x => x is Administrator
                        ? TenantRoleType.Administrator
                        : x is Teacher
                            ? TenantRoleType.Teacher
                            : x is Student
                                ? TenantRoleType.Student
                                : x is Parent
                                    ? TenantRoleType.Parent
                                    : default).ToList()
                })
                .ToListAsync(cancellationToken);
            return members;
        }
    }
}
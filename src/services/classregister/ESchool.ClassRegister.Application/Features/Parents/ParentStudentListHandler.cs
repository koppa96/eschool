using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Parents;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Parents
{
    public class ParentStudentListHandler : IRequestHandler<ParentStudentListQuery, List<UserRoleListResponse>>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public ParentStudentListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }

        public Task<List<UserRoleListResponse>> Handle(ParentStudentListQuery request,
            CancellationToken cancellationToken)
        {
            return context.Parents.Where(x => x.Id == request.ParentId)
                .SelectMany(x => x.StudentParents.Select(sp => sp.Student))
                .ProjectTo<UserRoleListResponse>(configurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
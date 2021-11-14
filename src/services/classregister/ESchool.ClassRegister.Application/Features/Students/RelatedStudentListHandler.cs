using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Interface.Features.Students;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Students
{
    public class RelatedStudentListHandler : IRequestHandler<RelatedStudentListQuery, List<UserRoleListResponse>>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public RelatedStudentListHandler(
            ClassRegisterContext context,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<List<UserRoleListResponse>> Handle(RelatedStudentListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var students = new List<Student>();
            
            if (identityService.IsInRole(TenantRoleType.Student))
            {
                students.Add(await context.Students.Include(x => x.User)
                    .SingleAsync(x => x.UserId == currentUserId, cancellationToken));
            }

            if (identityService.IsInRole(TenantRoleType.Parent))
            {
                var studentsOfParent = await context.Parents
                    .Where(x => x.UserId == currentUserId)
                    .SelectMany(x => x.StudentParents.Select(x => x.Student))
                    .Include(x => x.User)
                    .ToListAsync(cancellationToken);

                students.AddRange(studentsOfParent);
            }

            return mapper.Map<List<UserRoleListResponse>>(students);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Users.Teachers;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users.Teachers
{
    public class TeacherListHandler : IRequestHandler<TeacherListQuery, List<UserRoleListResponse>>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public TeacherListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public async Task<List<UserRoleListResponse>> Handle(TeacherListQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await context.UserRoles.ToListAsync(cancellationToken);
            var teachers = await context.Teachers.Include(x => x.User).ToListAsync(cancellationToken);
            
            return teachers.Where(x => x.User.Name.ToLower().Contains(request.SearchText.ToLower()))
                .Select(x => new UserRoleListResponse
                {
                    Id = x.Id,
                    Name = x.User.Name
                })
                .ToList();
        }
    }
}
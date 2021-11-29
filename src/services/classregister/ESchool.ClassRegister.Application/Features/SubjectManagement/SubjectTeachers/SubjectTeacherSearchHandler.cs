using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.SubjectTeachers;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.SubjectTeachers
{
    public class SubjectTeacherSearchHandler : IRequestHandler<SubjectTeacherSearchQuery, List<UserRoleListResponse>>
    {
        private readonly ClassRegisterContext context;

        public SubjectTeacherSearchHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public Task<List<UserRoleListResponse>> Handle(SubjectTeacherSearchQuery request, CancellationToken cancellationToken)
        {
            return context.SubjectTeachers.Where(x =>
                    x.SubjectId == request.SubjectId &&
                    x.Teacher.User.Name.ToLower().Contains(request.SearchText.ToLower()))
                .Select(x => new UserRoleListResponse
                {
                    Id = x.TeacherId,
                    Name = x.Teacher.User.Name
                }).ToListAsync(cancellationToken);
        }
    }
}
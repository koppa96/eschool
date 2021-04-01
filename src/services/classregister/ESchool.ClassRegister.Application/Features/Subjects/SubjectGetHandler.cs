using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Subjects.Common;
using ESchool.ClassRegister.Application.Features.Users.Common;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectGetQuery : IRequest<SubjectDetailsResponse>
    {
        public Guid Id { get; set; }        
    }
    
    public class SubjectGetHandler : IRequestHandler<SubjectGetQuery, SubjectDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public SubjectGetHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<SubjectDetailsResponse> Handle(SubjectGetQuery request, CancellationToken cancellationToken)
        {
            var subject = await context.Subjects.Include(x => x.SubjectTeachers)
                    .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            return new SubjectDetailsResponse
            {
                Id = subject.Id,
                Name = subject.Name,
                Teachers = subject.SubjectTeachers.Select(x => new UserRoleListResponse
                {
                    Id = x.TeacherId,
                    Name = x.Teacher.User.Name
                }).ToList()
            };
        }
    }
}
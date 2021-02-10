using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomListQuery : IRequest<List<ClassroomListResponse>>
    {
    }

    public class ClassroomListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    
    public class ClassroomListHandler : IRequestHandler<ClassroomListQuery, List<ClassroomListResponse>>
    {
        private readonly ClassRegisterContext context;

        public ClassroomListHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public Task<List<ClassroomListResponse>> Handle(ClassroomListQuery request, CancellationToken cancellationToken)
        {
            return context.ClassRooms.Select(x => new ClassroomListResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);
        }
    }
}
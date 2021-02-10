using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectListQuery : IRequest<List<SubjectListResponse>>
    {
    }

    public class SubjectListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class SubjectListHandler : IRequestHandler<SubjectListQuery, List<SubjectListResponse>>
    {
        public SubjectListHandler()
        {
            
        }
        
        public Task<List<SubjectListResponse>> Handle(SubjectListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
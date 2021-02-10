using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearListQuery : PagedListQuery, IRequest<PagedListResponse<SchoolYearListResponse>>
    {
    }

    public class SchoolYearListResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
    }
    
    public class SchoolYearListHandler : IRequestHandler<SchoolYearListQuery, PagedListResponse<SchoolYearListResponse>>
    {
        private readonly ClassRegisterContext context;

        public SchoolYearListHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<PagedListResponse<SchoolYearListResponse>> Handle(SchoolYearListQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await context.SchoolYears.CountAsync(cancellationToken);
            
            var responses = new List<SchoolYearListResponse>();
            if (totalCount > request.PageIndex * request.PageSize)
            {
                responses = await context.SchoolYears.OrderByDescending(x => x.DisplayName)
                    .Skip(request.PageIndex * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new SchoolYearListResponse { Id = x.Id, DisplayName = x.DisplayName })
                    .ToListAsync(cancellationToken);
            }

            return new PagedListResponse<SchoolYearListResponse>
            {
                Items = responses,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
            
        }
    }
}
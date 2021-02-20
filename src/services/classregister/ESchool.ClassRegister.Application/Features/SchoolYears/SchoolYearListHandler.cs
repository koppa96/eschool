using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearListQuery : PagedListQuery<SchoolYearListResponse>
    {
    }

    public class SchoolYearListResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
    }
    
    public class SchoolYearListHandler : PagedListHandler<SchoolYearListQuery, SchoolYear, string, SchoolYearListResponse>
    {
        public SchoolYearListHandler(ClassRegisterContext context) : base(context)
        {
        }

        protected override Expression<Func<SchoolYear, string>> OrderBy => x => x.DisplayName;

        protected override Expression<Func<SchoolYear, SchoolYearListResponse>> Select => x =>
            new SchoolYearListResponse
            {
                Id = x.Id,
                DisplayName = x.DisplayName
            };
    }
}
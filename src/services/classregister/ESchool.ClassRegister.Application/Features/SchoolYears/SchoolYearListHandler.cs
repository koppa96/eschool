using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.Libs.Application.Cqrs.Handlers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SchoolYears
{
    public class SchoolYearListHandler : AutoMapperPagedListHandler<SchoolYearListQuery, SchoolYear, SchoolYearListResponse>
    {
        public SchoolYearListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }
        
        protected override IOrderedQueryable<SchoolYear> Order(IQueryable<SchoolYear> entities)
        {
            return entities.OrderBy(x => x.DisplayName);
        }
    }
}
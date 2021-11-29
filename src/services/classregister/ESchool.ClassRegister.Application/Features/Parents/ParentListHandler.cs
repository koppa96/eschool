using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Interface.Features.Parents;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Response.Common;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Parents
{
    public class ParentListHandler : AutoMapperPagedListHandler<ParentListQuery, Parent, UserRoleListResponse>
    {
        public ParentListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IOrderedQueryable<Parent> Order(IQueryable<Parent> entities, ParentListQuery query)
        {
            return entities.OrderBy(x => x.User.Name);
        }
    }
}
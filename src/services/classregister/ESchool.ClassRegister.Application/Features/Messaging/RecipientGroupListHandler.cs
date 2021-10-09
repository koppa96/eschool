using System;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class RecipientGroupListQuery : PagedListQuery<RecipientGroupListResponse>
    {
        public string SearchText { get; set; }
    }

    public class RecipientGroupListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class RecipientGroupListHandler : AutoMapperPagedListHandler<RecipientGroupListQuery, RecipientGroup, RecipientGroupListResponse>
    {
        public RecipientGroupListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
            : base(context, configurationProvider)
        {
        }

        protected override IQueryable<RecipientGroup> Filter(IQueryable<RecipientGroup> entities, RecipientGroupListQuery query)
        {
            return !string.IsNullOrEmpty(query.SearchText)
                ? entities.Where(x => x.Name.ToLower().Contains(query.SearchText.ToLower()))
                : entities;
        }

        protected override IOrderedQueryable<RecipientGroup> Order(IQueryable<RecipientGroup> entities, RecipientGroupListQuery query)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}
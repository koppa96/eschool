using System;
using System.Linq;
using AutoMapper;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Query;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Domain.Entities;

namespace ESchool.Messaging.Application.Features.RecipientGroups
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
        private readonly IIdentityService identityService;

        public RecipientGroupListHandler(MessagingContext context, IConfigurationProvider configurationProvider, IIdentityService identityService)
            : base(context, configurationProvider)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<RecipientGroup> Filter(IQueryable<RecipientGroup> entities, RecipientGroupListQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return !string.IsNullOrEmpty(query.SearchText)
                ? entities.Where(x => x.Name.ToLower().Contains(query.SearchText.ToLower()) && x.UserId == currentUserId)
                : entities.Where(x => x.UserId == currentUserId);
        }

        protected override IOrderedQueryable<RecipientGroup> Order(IQueryable<RecipientGroup> entities, RecipientGroupListQuery query)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}
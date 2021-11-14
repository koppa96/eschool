using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Response;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Interface.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Messaging.Application.Features.Messages
{
    public class MessageRecipientListHandler : IRequestHandler<MessageRecipientListQuery, PagedListResponse<RecipientDto>>
    {
        private readonly MessagingContext context;
        private readonly IIdentityService identityService;

        public MessageRecipientListHandler(MessagingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<PagedListResponse<RecipientDto>> Handle(MessageRecipientListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var totalCount = await context.Users.CountAsync(cancellationToken) +
                             await context.RecipientGroups.CountAsync(x => x.UserId == currentUserId,
                                 cancellationToken);
            
            var responses = new List<RecipientDto>();
            if (totalCount > request.PageIndex * request.PageSize)
            {
                responses = await context.Users.Select(x => new RecipientDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = RecipientDto.RecipientType.User
                }).Concat(context.RecipientGroups.Select(x => new RecipientDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = RecipientDto.RecipientType.Group
                }))
                    .Where(x => string.IsNullOrEmpty(request.SearchText) || x.Name.ToLower().Contains(request.SearchText))
                    .OrderBy(x => x.Name)
                    .Skip(request.PageIndex * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);
            }

            return new PagedListResponse<RecipientDto>
            {
                Items = responses,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Application.Cqrs.Authorization.PipelineBehaviors
{
    public class AuthorizingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IServiceProvider serviceProvider;

        public AuthorizingPipelineBehaviour(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizationHandler = serviceProvider.GetService<IRequestAuthorizationHandler<TRequest>>();
            if (authorizationHandler == null)
            {
                return await next();
            }

            var result = await authorizationHandler.IsAuthorizedAsync(request, cancellationToken);
            if (result.AuthorizationSuccessful)
            {
                return await next();
            }

            throw new UnauthorizedAccessException(result.ErrorMessage);

        }
    }
}
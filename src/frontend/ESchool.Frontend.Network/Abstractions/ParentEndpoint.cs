using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Frontend.Network.Abstractions
{
    public class ParentEndpoint<TChildEndpointSelector>
    {
        private readonly ConstructorInfo childEndpointConstructorInfo = typeof(TChildEndpointSelector)
            .GetConstructors()
            .Single();

        private readonly ParameterInfo[] childEndpointConstructorParams;

        private readonly IServiceProvider serviceProvider;
        private readonly Dictionary<Guid, TChildEndpointSelector> childEndpointSelectors = new();

        public TChildEndpointSelector this[Guid resourceId]
        {
            get
            {
                if (childEndpointSelectors.ContainsKey(resourceId))
                {
                    return childEndpointSelectors[resourceId];
                }

                var @params = childEndpointConstructorParams
                    .Select(x => x.ParameterType == typeof(Guid)
                        ? resourceId
                        : serviceProvider.GetRequiredService(x.ParameterType))
                    .ToArray();

                var childEndpointSelector = (TChildEndpointSelector)childEndpointConstructorInfo.Invoke(@params);
                childEndpointSelectors.Add(resourceId, childEndpointSelector);
                return childEndpointSelector;
            }
        }

        public ParentEndpoint(IServiceProvider serviceProvider)
        {
            childEndpointConstructorParams = childEndpointConstructorInfo.GetParameters();
            
            this.serviceProvider = serviceProvider;
        }
    }
}
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Frontend.Network.Abstractions
{
    public class ChildEndpointFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ChildEndpointFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TChildEndpoint CreateChildEndpoint<TChildEndpoint>(string basePath)
        {
            return CreateWithBasePath<TChildEndpoint>(basePath);
        }

        public TChildEndpointSelector CreateChildEndpointSelector<TChildEndpointSelector>(string basePath)
        {
            return CreateWithBasePath<TChildEndpointSelector>(basePath);
        }

        private T CreateWithBasePath<T>(string basePath)
        {
            var constructorInfo = typeof(T).GetConstructors()
                .Single();

            var constructorParameters = constructorInfo.GetParameters()
                .Select(x => x.ParameterType == typeof(string) 
                    ? basePath 
                    : serviceProvider.GetRequiredService(x.ParameterType))
                .ToArray();

            return (T)constructorInfo.Invoke(constructorParameters);
        }
    }
}
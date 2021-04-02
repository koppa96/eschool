using System;
using System.Linq;
using ESchool.Libs.Outbox.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox
{
    public interface IOutboxConfigurator
    {
        public IServiceCollection Services { get; }

        public IOutboxConfigurator AddPublishFilter<TFilter, TMessage>()
            where TFilter : IPublishFilter<TMessage>
        {
            return AddPublishFilter(typeof(TFilter));
        }

        public IOutboxConfigurator AddPublishFilter(Type filterType)
        {
            var interfaces = filterType.GetInterfaces().Where(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPublishFilter<>));

            foreach (var @interface in interfaces)
            {
                Services.AddTransient(@interface, filterType);
            }

            return this;
        }
    }
}
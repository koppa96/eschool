using System;
using System.Linq;
using ESchool.Libs.Outbox.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox
{
    public interface IOutboxConfigurator
    {
        public IServiceCollection Services { get; }

        public IOutboxConfigurator AddPublishFilter<TFilter>()
            where TFilter : IPublishFilter
        {
            return AddPublishFilter(typeof(TFilter));
        }

        public IOutboxConfigurator AddPublishFilter(Type filterType)
        {
            Services.AddTransient(typeof(IPublishFilter), filterType);
            return this;
        }
    }
}
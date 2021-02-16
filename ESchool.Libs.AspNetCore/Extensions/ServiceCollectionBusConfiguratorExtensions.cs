using ESchool.Libs.AspNetCore.Filters;
using MassTransit;
using MassTransit.Registration;

namespace ESchool.Libs.AspNetCore.Extensions
{
    public static class ServiceCollectionBusConfiguratorExtensions
    {
        public static IBusFactoryConfigurator UseCustomFilters(
            this IBusFactoryConfigurator configurator, IConfigurationServiceProvider provider)
        {
            configurator.UseConsumeFilter(typeof(MessageLoggerConsumeFilter<>), provider);
            configurator.UseConsumeFilter(typeof(AuthDataGetterConsumeFilter<>), provider);
            configurator.UsePublishFilter(typeof(AuthDataSetterPublishFilter<>), provider);
            return configurator;
        }
    }
}
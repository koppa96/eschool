using ESchool.Testing.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Testing.Api.Extensions
{
    public static class JsonOptionsExtensions
    {
        public static void AddTaskTypeDiscriminatorConverterForHierarchy<TBaseClass>(this JsonOptions options)
        {
            foreach (var converter in TaskTypeDiscriminatorConverter.ForHierarchy<TBaseClass>())
            {
                options.JsonSerializerOptions.Converters.Add(converter);
            }
        }
    }
}
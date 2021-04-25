using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ESchool.Libs.Json.Attributes;
using ESchool.Libs.Json.Converters;

namespace ESchool.Libs.Json.Extensions
{
    public static class JsonOptionsExtensions
    {
        public static void AddDiscriminatorConverterForHierarchy<TBaseClass>(
            this JsonSerializerOptions options,
            string discriminatorPropertyName = JsonConstants.DefaultDiscriminator,
            params Assembly[] assembliesToSearch)
        {
            var converters = typeof(TBaseClass).Assembly.GetTypes()
                .Where(x => x.IsAssignableTo(typeof(TBaseClass)))
                .Select(x => (JsonConverter)Activator.CreateInstance(
                    typeof(DiscriminatorConverter<>).MakeGenericType(x),
                    discriminatorPropertyName,
                    assembliesToSearch));
            
            foreach (var converter in converters)
            {
                options.Converters.Add(converter);
            }
        }

        public static void AddDiscriminatorConverters(this JsonSerializerOptions options,
            params Assembly[] assemblies)
        {
            var baseClasses = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => x.GetCustomAttribute<JsonBaseClassAttribute>() != null);

            foreach (var baseClass in baseClasses)
            {
                typeof(JsonOptionsExtensions)
                    .GetMethod(nameof(AddDiscriminatorConverterForHierarchy))!
                    .MakeGenericMethod(baseClass)
                    .Invoke(null, new object[]
                    {
                        options, 
                        baseClass.GetCustomAttribute<JsonBaseClassAttribute>()!.DiscriminatorPropertyName ?? JsonConstants.DefaultDiscriminator,
                        assemblies
                    });
            }
        }
    }
}
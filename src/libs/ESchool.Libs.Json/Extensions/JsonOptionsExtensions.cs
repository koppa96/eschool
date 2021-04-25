using System.Text.Json;
using ESchool.Libs.Json.Converters;

namespace ESchool.Libs.Json.Extensions
{
    public static class JsonOptionsExtensions
    {
        public static void AddDiscriminatorConverterForHierarchy<TBaseClass>(
            this JsonSerializerOptions options,
            string discriminatorPropertyName = "discriminator")
        {
            foreach (var converter in TaskTypeDiscriminatorConverter.ForHierarchy<TBaseClass>(discriminatorPropertyName))
            {
                options.Converters.Add(converter);
            }
        }
    }
}
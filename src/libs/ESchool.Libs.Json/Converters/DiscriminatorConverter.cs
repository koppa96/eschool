using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ESchool.Libs.Json.Attributes;

namespace ESchool.Libs.Json.Converters
{
    public static class DiscriminatorConverter
    {
        public static IEnumerable<JsonConverter> ForHierarchy<TBaseClass>(string discriminatorPropertyName)
        {
            return typeof(TBaseClass).Assembly.GetTypes()
                .Where(x => x.IsAssignableTo(typeof(TBaseClass)))
                .Select(x => (JsonConverter)Activator.CreateInstance(typeof(DiscriminatorConverter<>).MakeGenericType(x), discriminatorPropertyName));
        }
    }
    
    /// <summary>
    /// Appends a TaskType property to the serialized type based on the TaskTypeAttribute on the class,
    /// and deserializes polymorphic types with the help of it.
    /// </summary>
    /// <typeparam name="TBaseClass">The type of the base class</typeparam>
    public class DiscriminatorConverter<TBaseClass> : JsonConverter<TBaseClass>
    {
        private readonly string discriminatorPropertyName;
        private readonly List<Type> subTypes;

        public DiscriminatorConverter(string discriminatorPropertyName, params Assembly[] assembliesToSearch)
        {
            this.discriminatorPropertyName = discriminatorPropertyName;

            subTypes = assembliesToSearch.Append(typeof(TBaseClass).Assembly)
                .Distinct()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsAssignableTo(typeof(TBaseClass)) &&
                            x.GetCustomAttribute<DiscriminatorAttribute>() != null)
                .ToList();
        }

        public override TBaseClass Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);

            var discriminatorProperty = document.RootElement.EnumerateObject().Single(x => x.NameEquals(discriminatorPropertyName));
            var discriminatorValue = discriminatorProperty.Value.GetString();
            var objectType = subTypes.Single(x => x.GetCustomAttribute<DiscriminatorAttribute>()!.DiscriminatorValue == discriminatorValue);
            
            using var stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream);
            document.WriteTo(writer);
            writer.Flush();
            var jsonString = Encoding.UTF8.GetString(stream.ToArray());

            var newOptions = new JsonSerializerOptions(options);
            newOptions.Converters.Remove(this);

            var childConverterType = typeof(DiscriminatorConverter<>).MakeGenericType(objectType);
            var childConverter = options.Converters.SingleOrDefault(x => x.GetType() == childConverterType);
            newOptions.Converters.Remove(childConverter);

            return (TBaseClass) JsonSerializer.Deserialize(jsonString, objectType, newOptions);
        }

        public override void Write(Utf8JsonWriter writer, TBaseClass value, JsonSerializerOptions options)
        {
            var newOptions = new JsonSerializerOptions(options);
            newOptions.Converters.Remove(this);
            
            var jsonString = JsonSerializer.Serialize(value, value.GetType(), newOptions);
            using var document = JsonDocument.Parse(jsonString);

            writer.WriteStartObject();
            writer.WriteString(discriminatorPropertyName, value.GetType().GetCustomAttribute<DiscriminatorAttribute>()?.DiscriminatorValue);
            foreach (var property in document.RootElement.EnumerateObject())
            {
                property.WriteTo(writer);
            }
            writer.WriteEndObject();
        }
    }
}
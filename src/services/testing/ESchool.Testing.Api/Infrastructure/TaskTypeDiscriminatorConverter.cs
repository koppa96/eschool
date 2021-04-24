using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ESchool.Testing.Domain.Attributes;
using ESchool.Testing.Domain.Enums;

namespace ESchool.Testing.Api.Infrastructure
{
    public static class TaskTypeDiscriminatorConverter
    {
        public static IEnumerable<JsonConverter> ForHierarchy<TBaseClass>()
        {
            return typeof(TBaseClass).Assembly.GetTypes()
                .Where(x => x.IsAssignableTo(typeof(TBaseClass)))
                .Select(x => (JsonConverter)Activator.CreateInstance(typeof(TaskTypeDiscriminatorConverter<>).MakeGenericType(x)));
        }
    }
    
    /// <summary>
    /// Appends a TaskType property to the serialized type based on the TaskTypeAttribute on the class,
    /// and deserializes polymorphic types with the help of it.
    /// </summary>
    /// <typeparam name="TBaseClass">The type of the base class</typeparam>
    public class TaskTypeDiscriminatorConverter<TBaseClass> : JsonConverter<TBaseClass>
    {
        public const string Discriminator = "taskType";

        public override TBaseClass Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);

            var taskTypeProperty = document.RootElement.EnumerateObject().Single(x => x.NameEquals(Discriminator));
            var taskTypeName = taskTypeProperty.Value.GetString();
            var objectType = typeof(TBaseClass).Assembly.GetTypes()
                .Single(x => x.IsAssignableTo(typeof(TBaseClass)) &&
                             x.GetCustomAttribute<TaskTypeAttribute>()?.TaskType.Name == taskTypeName);
            
            using var stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream);
            document.WriteTo(writer);
            writer.Flush();
            var jsonString = Encoding.UTF8.GetString(stream.ToArray());

            var newOptions = new JsonSerializerOptions(options);
            newOptions.Converters.Remove(this);
            
            return (TBaseClass) JsonSerializer.Deserialize(jsonString, objectType, newOptions);
        }

        public override void Write(Utf8JsonWriter writer, TBaseClass value, JsonSerializerOptions options)
        {
            var newOptions = new JsonSerializerOptions(options);
            newOptions.Converters.Remove(this);
            
            var jsonString = JsonSerializer.Serialize(value, value.GetType(), newOptions);
            using var document = JsonDocument.Parse(jsonString);

            writer.WriteStartObject();
            writer.WriteString(Discriminator, value.GetType().GetCustomAttribute<TaskTypeAttribute>()?.TaskType.Name);
            foreach (var property in document.RootElement.EnumerateObject())
            {
                property.WriteTo(writer);
            }
            writer.WriteEndObject();
        }
    }
}
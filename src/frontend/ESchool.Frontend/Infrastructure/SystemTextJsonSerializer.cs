using System.IO;
using System.Text.Json;
using Flurl.Http.Configuration;

namespace ESchool.Frontend.Infrastructure
{
    public class SystemTextJsonSerializer : ISerializer
    {
        private readonly JsonSerializerOptions options;

        public SystemTextJsonSerializer(JsonSerializerOptions options)
        {
            this.options = options;
        }
        
        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, obj.GetType(), options);
        }

        public T Deserialize<T>(string s)
        {
            return JsonSerializer.Deserialize<T>(s, options);
        }

        public T Deserialize<T>(Stream stream)
        {
            return JsonSerializer.DeserializeAsync<T>(stream, options)
                .AsTask()
                .GetAwaiter()
                .GetResult();
        }
    }
}
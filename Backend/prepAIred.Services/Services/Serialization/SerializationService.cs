using System.Text.Json;
using System.Text.Json.Serialization;

namespace prepAIred.Services
{
    public class SerializationService : ISerializationService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        public string Serialize<T>(T obj) where T : class => JsonSerializer.Serialize(obj ?? throw new ArgumentNullException(nameof(obj)), _jsonOptions);

        public string SerializeCollection<T>(IEnumerable<T> objects) where T : class => JsonSerializer.Serialize(objects ?? throw new ArgumentNullException(nameof(objects)), _jsonOptions);

        public T? Deserialize<T>(string json) where T : class => JsonSerializer.Deserialize<T>(!string.IsNullOrEmpty(json) ? json : throw new ArgumentNullException(nameof(json)), _jsonOptions);

        public ICollection<T>? DeserializeCollection<T>(string json) where T : class => JsonSerializer.Deserialize<ICollection<T>>(!string.IsNullOrEmpty(json) ? json : throw new ArgumentNullException(nameof(json)), _jsonOptions);
    }
}

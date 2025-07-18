
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InnoSoft.InventorySystem.Extensions
{
    public static class AppJsonSerializerExtensions
    {
        private static readonly JsonSerializerOptions _defaultOptions;

        static AppJsonSerializerExtensions()
        {
            DefaultConverters = new JsonConverter[]
            {
                new JsonStringEnumConverter(),
            };
            _defaultOptions = new JsonSerializerOptions();
            DefaultConverters.ForEach(_defaultOptions.Converters.Add);
            _defaultOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        }

        public static IEnumerable<JsonConverter> DefaultConverters { get; }

        public static string SerializeWithDefaultOptions(object value)
        {
            if (value == null)
                return null;

            return JsonSerializer.Serialize(value, value.GetType(), _defaultOptions);
        }

        public static T DeserializeWithDefaultOptions<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _defaultOptions);
        }
    }
}

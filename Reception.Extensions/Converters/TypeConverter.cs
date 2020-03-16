using Newtonsoft.Json;
using System;

namespace Reception.Extensions.Converters
{
    public class TypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type type = (Type)value;
            writer.WriteValue(type.AssemblyQualifiedName);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Type type;
            if (reader.Value is string typeName)
            {
                type = Type.GetType(typeName);
            }
            else
            {
                type = typeof(object);
            }
            return type;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Type);
        }
    }
}
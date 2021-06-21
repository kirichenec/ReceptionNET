using Newtonsoft.Json;
using System;

namespace Reception.Extension.Converters
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
            Type type = null;
            if (reader.Value is string typeName)
            {
                type = Type.GetType(typeName);
            }
            return type;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Type);
        }
    }
}
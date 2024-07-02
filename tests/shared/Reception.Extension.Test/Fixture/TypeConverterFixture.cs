using Newtonsoft.Json;
using Reception.Extension.Converters;

namespace Reception.Extension.Test.Fixture;

internal static class TypeConverterFixture
{
    internal static JsonReader GetJsonReader(string json)
    {
        var sr = new StringReader(json);
        var jsonReader = new JsonTextReader(sr);
        jsonReader.Read();
        return jsonReader;
    }

    internal static TypeConverter GetTypeConverter()
    {
        return new TypeConverter();
    }

    internal static (JsonWriter jsonWrriter, JsonSerializer serializer, TypeConverter typeConverter, StringWriter stringWriter) GetWriterObjects()
    {
        var sw = new StringWriter();
        var jsonWriter = new JsonTextWriter(sw);
        var serializer = new JsonSerializer();

        var typeConverter = GetTypeConverter();

        return (jsonWriter, serializer, typeConverter, sw);
    }
}

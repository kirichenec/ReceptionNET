using Newtonsoft.Json;
using Reception.Extension.Test.Fixture;

namespace Reception.Extension.Converters.Test;

public class TypeConverterTests
{
    public static TheoryData<Type> CorrectSourceDataForWriteJson => new(typeof(object), typeof(TypeConverterTests), typeof(Type));

    public static TheoryData<string> CorrectJsonWithoutTypeName => new("", "\"notTypeName\"", "[]");

    public static TheoryData<string, Type> CorrectJsonWithTypeNameForReadJson => new()
    {
        { $"\"{typeof(object).AssemblyQualifiedName}\"",             typeof(object) },
        { $"\"{typeof(TypeConverterTests).AssemblyQualifiedName}\"", typeof(TypeConverterTests) },
        { $"\"{typeof(Type).AssemblyQualifiedName}\"",               typeof(Type) },
    };

    public static TheoryData<Type, bool> TypesWithExpectedForCanConvert => new()
    {
        { typeof(Type),               true },
        { null,                       false },
        { typeof(object),             false },
        { typeof(TypeConverterTests), false },
    };


    [Theory, MemberData(nameof(CorrectSourceDataForWriteJson))]
    public void TypeConverter_WriteJson_NotThrowing(object sourceValue)
    {
        // arrange
        var (jsonWriter, serializer, typeConverter, _) = TypeConverterFixture.GetWriterObjects();

        // act
        var act = () => typeConverter.WriteJson(jsonWriter, sourceValue, serializer);

        // assert
        act.Should().NotThrow();
    }

    [Fact]
    public void TypeConverter_WriteJson_SourceValueIsNull_ShouldThrow()
    {
        // arrange
        object sourceValue = null;
        var (jsonWriter, serializer, typeConverter, _) = TypeConverterFixture.GetWriterObjects();

        // act
        var act = () => typeConverter.WriteJson(jsonWriter, sourceValue, serializer);

        // assert
        act.Should().Throw<NullReferenceException>();
    }

    [Theory, MemberData(nameof(CorrectSourceDataForWriteJson))]
    public void TypeConverter_WriteJson_ReturnExpectedWriter(object sourceValue)
    {
        // arrange
        var (jsonWriter, serializer, typeConverter, stringWriter) = TypeConverterFixture.GetWriterObjects();
        var expectedResult = $"\"{(sourceValue as Type).AssemblyQualifiedName}\"";

        // act
        typeConverter.WriteJson(jsonWriter, sourceValue, serializer);
        var result = stringWriter.ToString();

        // assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Fact]
    public void TypeConverter_ReadJson_JsonReaderIsNull_ShouldThrow()
    {
        // arrange
        JsonReader jsonReader = null;
        var typeConverter = TypeConverterFixture.GetTypeConverter();

        // act
        var act = () => typeConverter.ReadJson(jsonReader, null, null, null);

        // assert
        act.Should().Throw<NullReferenceException>();
    }

    [Theory, MemberData(nameof(CorrectJsonWithoutTypeName))]
    public void TypeConverter_ReadJson_SourceValueIsNotTypeName_NotThrowing(string sourceJson)
    {
        // arrange
        var jsonReader = TypeConverterFixture.GetJsonReader(sourceJson);
        var typeConverter = TypeConverterFixture.GetTypeConverter();

        // act
        var act = () => typeConverter.ReadJson(jsonReader, null, null, null);

        // assert
        act.Should().NotThrow();
    }

    [Theory, MemberData(nameof(CorrectJsonWithTypeNameForReadJson))]
    public void TypeConverter_ReadJson_ReturnsExpected(string sourceJson, Type expectedType)
    {
        // arrange
        var jsonReader = TypeConverterFixture.GetJsonReader(sourceJson);
        var typeConverter = TypeConverterFixture.GetTypeConverter();

        // act
        var result = typeConverter.ReadJson(jsonReader, null, null, null);

        // assert
        result.Should().BeEquivalentTo(expectedType);
    }


    [Fact]
    public void TypeConverter_CanConvert_SourceValueIsNull_NotThrowing()
    {
        // arrange
        Type sourceValue = null;
        var typeConverter = TypeConverterFixture.GetTypeConverter();

        // act
        var act = () => typeConverter.CanConvert(sourceValue);

        // assert
        act.Should().NotThrow();
    }

    [Theory, MemberData(nameof(TypesWithExpectedForCanConvert))]
    public void TypeConverter_CanConvert_ReturnsExpected(Type sourceType, bool expectedCanConvert)
    {
        // arrange
        var typeConverter = TypeConverterFixture.GetTypeConverter();

        // act
        var result = typeConverter.CanConvert(sourceType);

        // Assert
        result.Should().Be(expectedCanConvert);
    }
}

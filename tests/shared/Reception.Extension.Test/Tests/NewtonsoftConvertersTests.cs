using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Reception.Extension.Converters.Test;

[SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable", Justification = "<Pending>")]
[SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows", Justification = "<Pending>")]
public class NewtonsoftConvertersTests
{
    public static TheoryData<string> CorrectSourceDataForDeserialization => new("", "{}", "[]");

    public static TheoryData<string> IncorrectSourceDataForDeserialization => new("sfasdsd", "{]");

    public static TheoryData<string, object> SourceDataWithExpectedForDeserialization => new()
    {
        { "",                     null },
        { "[]",                   Array.Empty<int>() },
        { "[ 1, 2 ]",             new string[] { "1", "2" } },
        { """[ "one", "two" ]""", new string[] { "one", "two" } },
        { "[ 1, 2 ]",             new int[] { 1, 2 } },
    };

    public static IEnumerable<object[]> CorrectIEnumerableData =>
    [
        [null],
        [new List<int?> { null }],
        [new List<int?> { 3 }],
        [new List<string> { "" }],
    ];

    public static IEnumerable<object[]> IEnumerableDataWithExpectedForToJoinStrings =>
    [
        [null,                              null],
        [new List<int?> { null },           new string[1] { "null" }],
        [new List<int?> { 3 },              new string[1] { "3" }],
        [new List<string> { "" },           new string[1] { "\"\"" }],
        [new List<string> { "one", "two" }, new string[2] { "\"one\"", "\"two\"" }],
    ];


    [Theory, MemberData(nameof(CorrectSourceDataForDeserialization))]
    public void Newtonsoft_DeserializeObjectMessage_NotThrowing<Tout>(object sourceValue)
    {
        // act
        var act = sourceValue.DeserializeMessage<Tout>;

        // assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Newtonsoft_DeserializeObjectMessage_SourceValueIsNull_ShouldThrow()
    {
        // arrange
        object sourceValue = null;

        // act
        var act = sourceValue.DeserializeMessage<object>;

        // assert
        act.Should().Throw<NullReferenceException>();
    }

    [Theory, MemberData(nameof(IncorrectSourceDataForDeserialization))]
    public void Newtonsoft_DeserializeObjectMessage_SourceValueIsNotCorrect_ShouldThrow<Tout>(object sourceValue)
    {
        // act
        var act = sourceValue.DeserializeMessage<Tout>;

        // assert
        act.Should().Throw<JsonReaderException>();
    }


    [Theory, MemberData(nameof(CorrectSourceDataForDeserialization))]
    public void Newtonsoft_DeserializeStringMessage_NotThrowing<Tout>(string sourceValue)
    {
        // act
        var act = sourceValue.DeserializeMessage<Tout>;

        // assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Newtonsoft_DeserializeStringMessage_SourceValueIsNull_ShouldThrow()
    {
        // arrange
        string sourceValue = null;

        // act
        var act = sourceValue.DeserializeMessage<object>;

        // assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Theory, MemberData(nameof(IncorrectSourceDataForDeserialization))]
    public void Newtonsoft_DeserializeStringMessage_SourceValueIsNotCorrect_ShouldThrow<Tout>(string sourceValue)
    {
        // act
        var act = sourceValue.DeserializeMessage<Tout>;

        // assert
        act.Should().Throw<JsonReaderException>();
    }


    [Theory, MemberData(nameof(SourceDataWithExpectedForDeserialization))]
    public void Newtonsoft_DeserializeObjectMessage_ReturnsExpected<Tout>(object sourceValue, Tout expectedResult)
    {
        // Arrange
        var result = sourceValue.DeserializeMessage<Tout>();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Theory, MemberData(nameof(SourceDataWithExpectedForDeserialization))]
    public void Newtonsoft_DeserializeStringMessage_ReturnsExpected<Tout>(string sourceValue, Tout expectedResult)
    {
        // Arrange
        var result = sourceValue.DeserializeMessage<Tout>();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Fact]
    public void Newtonsoft_ToJsonString_SourceValueIsNull_NotThrowing()
    {
        // arrange
        int? sourceValue = null;

        // act
        var act = () => sourceValue.ToJsonString();

        // assert
        act.Should().NotThrow();
    }


    [Theory]
    [InlineData(null, "null")]
    [InlineData("", "\"\"")]
    [InlineData(1, "1")]
    [InlineData(new string[0] { }, "[]")]
    [InlineData(new string[1] { null }, "[null]")]
    [InlineData(new int[2] { 1, 2 }, "[1,2]")]
    public void Newtonsoft_ToJsonString_ReturnsExpected<Tin>(Tin sourceValue, string expectedResult)
    {
        // act
        var result = sourceValue.ToJsonString();

        // assert
        Assert.Equal(expectedResult, result);
    }


    [Theory]
    [InlineData(null)]
    [InlineData(new int[0])]
    [InlineData([new object[1] { null }])] // [null]
    public void Newtonsoft_ArrayToJsonStrings_NotThrowing<Tin>(Tin[] sourceValue)
    {
        // act
        var act = () => sourceValue.ToJsonStrings();

        // assert
        act.Should().NotThrow();
    }


    [Theory]
    [InlineData(null, null)]
    [InlineData(new int[0], new string[0])]
    [InlineData(new object[1] { null }, new string[1] { "null" })]
    [InlineData(new int[1] { 1 }, new string[1] { "1" })]
    public void Newtonsoft_ArrayToJsonStrings_ReturnsExpected<Tin>(Tin[] sourceValue, string[] expectedResult)
    {
        // act
        var result = sourceValue.ToJsonStrings();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Theory]
    [MemberData(nameof(CorrectIEnumerableData))]
    public void Newtonsoft_IEnumerableToJsonStrings_NotThrowing<T>(IEnumerable<T> sourceValue)
    {
        // act
        var act = () => sourceValue.ToJsonStrings();

        // assert
        act.Should().NotThrow();
    }


    [Theory]
    [MemberData(nameof(IEnumerableDataWithExpectedForToJoinStrings))]
    public void Newtonsoft_IEnumerableToJsonStrings_ReturnsExpected<Tin>(IEnumerable<Tin> sourceValue, string[] expectedResult)
    {
        // act
        var result = sourceValue.ToJsonStrings();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}

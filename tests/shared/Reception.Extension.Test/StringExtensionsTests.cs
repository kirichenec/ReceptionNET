using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Reception.Extension.Test
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("test")]
        public void StringExtensions_AsLike_NotThrowing(string sourceValue)
        {
            // Act
            var act = sourceValue.AsLike;

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(null, "%%")]
        [InlineData("", "%%")]
        [InlineData("test", "%test%")]
        public void StringExtensions_AsLike_ReturnsExpectedString(string sourceValue, string expectedResult)
        {
            // Arrange
            var result = sourceValue.AsLike();

            // Assert
            result.Should().Be(expectedResult);
        }


        [Theory]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData(null, null)]
        public void StringExtensions_CutEndBy_NotThrowing(string sourceValue, string cuttedValue)
        {
            // Act
            var act = () => sourceValue.CutEndBy(cuttedValue);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        // Any null returns source value
        [InlineData("", null, "")]
        [InlineData(null, "", null)]
        [InlineData(null, null, null)]
        [InlineData("value", null, "value")]
        // Source value contains cutted at the end
        [InlineData("value", "ue", "val")]
        [InlineData("value", "value", "")]
        [InlineData("valuevalue", "value", "value")]
        // Source value contains cutted not at the end
        [InlineData("value", "val", "value")]
        // Source value doesn't contain the cut value
        [InlineData("value", "valuevalue", "value")]
        [InlineData("value", "123", "value")]
        public void StringExtensions_CutEndBy_ReturnsExpectedString(
            string sourceValue, string cuttedValue, string expectedResult)
        {
            // Arrange
            var result = sourceValue.CutEndBy(cuttedValue);

            // Assert
            result.Should().Be(expectedResult);
        }


        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData(" f ", false)]
        [InlineData("value", false)]
        public void StringExtensions_IsNullOrWhiteSpace_ReturnsExpected(
            string sourceValue, bool expectedResult)
        {
            // Arrange
            var result = sourceValue.IsNullOrWhiteSpace();

            // Assert
            result.Should().Be(expectedResult);
        }


        [Fact]
        public void StringExtensions_ParseBool_SourceValueIsNull_ShouldThrow()
        {
            // Arrange
            string value = null;

            // Act
            var act = value.ParseBool;

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("123")]
        public void StringExtensions_ParseBool_SourceValueIsNotCorrect_ShouldThrow(string sourceValue)
        {
            // Act
            var act = sourceValue.ParseBool;

            // Assert
            act.Should().Throw<FormatException>();
        }

        [Theory]
        [InlineData("false", false)]
        [InlineData("False", false)]
        [InlineData("true", true)]
        [InlineData("True", true)]
        public void StringExtensions_ParseBool_ReturnsExpected(string sourceValue, bool expectedResult)
        {
            // Arrange
            var result = sourceValue.ParseBool();

            // Assert
            result.Should().Be(expectedResult);
        }


        [Fact]
        public void StringExtensions_ParseInt_SourceValueIsNull_ShouldThrow()
        {
            // Arrange
            string value = null;

            // Act
            var act = value.ParseInt;

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("asdf")]
        public void StringExtensions_ParseInt_SourceValueIsNotCorrect_ShouldThrow(string sourceValue)
        {
            // Act
            var act = sourceValue.ParseInt;

            // Assert
            act.Should().Throw<FormatException>();
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("2147483647", 2147483647)]
        [InlineData("-2147483648", -2147483648)]
        public void StringExtensions_ParseInt_ReturnsExpected(string sourceValue, int expectedResult)
        {
            // Arrange
            var result = sourceValue.ParseInt();

            // Assert
            result.Should().Be(expectedResult);
        }


        [Fact]
        public void StringExtensions_ToJoinString_SourceCollectionIsNull_ShouldThrow()
        {
            // Arrange
            string value = null;

            // Act
            var act = () => value.ToJoinString("");

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(new int[] { }, null, "")]
        [InlineData(new[] { 1, 2 }, null, "12")]
        [InlineData(new[] { 1, 2 }, " ", "1 2")]
        [InlineData(new[] { "One", "Two" }, "", "OneTwo")]
        [InlineData(new[] { "One", "Two" }, " ", "One Two")]
        [InlineData("One", " ", "O n e")]
        [InlineData("One", ", ", "O, n, e")]
        // Bug still not fixed
        // https://github.com/xunit/xunit/issues/2075
        [SuppressMessage("Usage", "xUnit1010:The value is not convertible to the method parameter type", Justification = "<Pending>")]
        public void StringExtensions_ToJoinString_ReturnsExpected<T>(
            IEnumerable<T> values, string separator, string expectedResult)
        {
            // Arrange
            var result = values.ToJoinString(separator);

            // Assert
            result.Should().Be(expectedResult);
        }


        [Fact]
        public void StringExtensions_ToTitleCase_SourceValueIsNull_ShouldThrow()
        {
            // Arrange
            string value = null;

            // Act
            var act = () => value.ToTitleCase();

            // Assert
            act.Should().Throw<NullReferenceException>();
        }

        [Theory]
        [InlineData("hello world", "en-US", "Hello World")]
        [InlineData("HELLO WORLD", "en-US", "Hello World")]
        // French ignore rules
        //[InlineData("les naufragés d'ythaq", "fr-FR", "Les Naufragés d'Ythaq")]
        //[InlineData("mon texte de démonstration", "fr-FR", "Mon Texte de Démonstration")]
        public void StringExtensions_ToTitleCase_ReturnsExpected(
            string sourceValue, string cultureName, string expectedResult)
        {
            // Arrange
            var cultureInfo = new CultureInfo(cultureName);

            // Act
            var act = () => sourceValue.ToTitleCase(cultureInfo);

            // Assert
            act().Should().Be(expectedResult);
        }
    }
}

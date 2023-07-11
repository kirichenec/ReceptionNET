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
    }
}

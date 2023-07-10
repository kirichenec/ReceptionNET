using System.Diagnostics.CodeAnalysis;

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
        // IDE's bug
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public void StringExtensions_AsLike_ReturnsExpectedString(string sourceValue, string expectedResult)
        {
            // Act
            var act = sourceValue.AsLike;

            // Assert
            act().Should().Be(expectedResult);
        }
    }
}

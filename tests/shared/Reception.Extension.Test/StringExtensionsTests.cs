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
    }
}

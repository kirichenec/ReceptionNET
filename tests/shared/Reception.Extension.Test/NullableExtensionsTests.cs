namespace Reception.Extension.Test
{
    public static class NullableExtensionsTests
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("", false)]
        public static void NullableExtensions_HasNoValue_ReturnsExpected<T>(
            T value, bool expextedResult) where T : class
        {
            // Arrange
            var result = value.HasNoValue();

            // Assert
            result.Should().Be(expextedResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", true)]
        public static void NullableExtensions_HasValue_ReturnsExpected<T>(
            T value, bool expextedResult) where T : class
        {
            // Arrange
            var result = value.HasValue();

            // Assert
            result.Should().Be(expextedResult);
        }
    }
}

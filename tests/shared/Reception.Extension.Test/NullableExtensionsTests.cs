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
            // Act
            var act = () => value.HasNoValue();

            // Assert
            act().Should().Be(expextedResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", true)]
        public static void NullableExtensions_HasValue_ReturnsExpected<T>(
            T value, bool expextedResult) where T : class
        {
            // Act
            var act = () => value.HasValue();

            // Assert
            act().Should().Be(expextedResult);
        }
    }
}

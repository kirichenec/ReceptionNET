using Reception.Extension.Test.Fixture;

namespace Reception.Extension.Test
{
    public class IQueryableExtensionsTests
    {
        [Fact]
        public void IQueryableExtensions_Paged_SourceValueIsNull_ShouldThrow()
        {
            // Arrange
            IQueryable<string> value = null;

            // Act
            var act = () => value.Paged(1, 10);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        [InlineData(-10, -10)]
        public void IQueryableExtensions_Paged_NonPositiveParams_ShouldNotThrow(int page, int count)
        {
            // Arrange
            var value = IQueryableExtensionsFixture.GetData(10);

            // Act
            var act = () => value.Paged(page, count);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(1, 1, 10, 1)]
        [InlineData(1, 10, 10, 10)]
        [InlineData(1, 100, 10, 10)]
        [InlineData(2, 10, 10, 0)]
        [InlineData(2, 10, 15, 5)]
        [InlineData(2, 5, 10, 5)]
        public void IQueryableExtensions_Paged_PositiveParams(int page, int count, uint sourceLength,
            int expectedCount)
        {
            // Arrange
            var value = IQueryableExtensionsFixture.GetData(sourceLength);

            // Act
            var act = () => value.Paged(page, count);

            // Assert
            act().Should().HaveCount(expectedCount);
        }
    }
}

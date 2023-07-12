﻿using Reception.Extension.Test.Fixture;

namespace Reception.Extension.Test
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void IEnumerableExtensions_ForEach_SourceIsNull_ShouldThrowNullReferenceException()
        {
            // Arrange
            IEnumerable<string> source = null;

            // Act
            var act = () => source.ForEach(x => { });

            // Assert
            act.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void IEnumerableExtensions_ForEach_ActionIsNull_ShouldNotThrow()
        {
            // Arrange
            var source = IEnumerableExtensionsFixture.GetData(0);
            Action<string> action = null;

            // Act
            var act = () => source.ForEach(action);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void IEnumerableExtensions_ForEach_CopyValuesToEmpty_ShouldBeEquivalentCollections()
        {
            // Arrange
            var source = IEnumerableExtensionsFixture.GetData(10);
            var values = new List<string>();

            // Act
            source.ForEach(values.Add);

            // Assert
            source.Should().BeEquivalentTo(values, options => options.WithStrictOrdering());
        }
    }
}

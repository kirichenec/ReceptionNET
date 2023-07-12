namespace Reception.Extension.Test
{
    public class DateTimeExtensionsTests
    {
        public static readonly object[][] InlineDataForBetween =
        {
            // input between start-end
            new object[] { new DateTime(2023, 03, 01), new DateTime(2023, 03, 01), new DateTime(2023, 03, 31), true },
            // input = start = end
            new object[] { new DateTime(2023, 03, 01), new DateTime(2023, 03, 01), new DateTime(2023, 03, 01), true },
            // start > end
            new object[] { new DateTime(2023, 03, 01), new DateTime(2023, 03, 31), new DateTime(2023, 03, 01), false },
            // input not between start-end
            new object[] { new DateTime(2024, 03, 01), new DateTime(2023, 03, 01), new DateTime(2023, 03, 31), false },
        };

        [Theory, MemberData(nameof(InlineDataForBetween))]
        public void DateTimeExtensions_Between_ReturnsExpected(DateTime input,
            DateTime startDateTime, DateTime endDateTime, bool expectedResult)
        {
            // Arrange
            var result = input.Between(startDateTime, endDateTime);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}

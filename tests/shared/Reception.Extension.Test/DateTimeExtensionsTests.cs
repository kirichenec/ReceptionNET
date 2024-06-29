namespace Reception.Extension.Test
{
    public class DateTimeExtensionsTests
    {
        public static readonly object[][] InlineDataForBetween =
        [
            // input between start-end
            [
                new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
                new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
                new DateTime(2023, 03, 31, 00, 00, 00, DateTimeKind.Local),
                true
            ],
            // input = start = end
            [
                new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
                new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
                new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
                true],
            // start > end
            [
                new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
                new DateTime(2023, 03, 31, 00, 00, 00, DateTimeKind.Local),
                new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
                false
            ],
            // input not between start-end
            [
                new DateTime(2024, 03, 01, 00, 00, 00, DateTimeKind.Local),
                new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
                new DateTime(2023, 03, 31, 00, 00, 00, DateTimeKind.Local),
                false
            ],
        ];

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

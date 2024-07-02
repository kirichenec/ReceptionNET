using Xunit.Abstractions;

namespace Reception.Extension.Test;

public class DateTimeExtensionsTests
{
    public static TheoryData<DateTimeExtensionInputItem> InlineDataForBetween =>
    [
        new()
        {
            Description =       "input between start-end",
            Input =             new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            StartDateTime =     new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            EndDateTime =       new DateTime(2023, 03, 31, 00, 00, 00, DateTimeKind.Local),
            ExpectedResult =    true,
        },
        new()
        {
            Description =       "input = start = end",
            Input =             new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            StartDateTime =     new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            EndDateTime =       new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            ExpectedResult =    true
        },
        new()
        {
            Description =       "start > end",
            Input =             new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            StartDateTime =     new DateTime(2023, 03, 31, 00, 00, 00, DateTimeKind.Local),
            EndDateTime =       new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            ExpectedResult =    false
        },
        new()
        {
            Description =       "input not between start-end",
            Input =             new DateTime(2024, 03, 01, 00, 00, 00, DateTimeKind.Local),
            StartDateTime =     new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            EndDateTime =       new DateTime(2023, 03, 31, 00, 00, 00, DateTimeKind.Local),
            ExpectedResult =    false
        },
        new()
        {
            Description =       "input not between start-end",
            Input =             new DateTime(2022, 03, 01, 00, 00, 00, DateTimeKind.Local),
            StartDateTime =     new DateTime(2023, 03, 01, 00, 00, 00, DateTimeKind.Local),
            EndDateTime =       new DateTime(2023, 03, 31, 00, 00, 00, DateTimeKind.Local),
            ExpectedResult =    false
        },
    ];


    [Theory, MemberData(nameof(InlineDataForBetween))]
    public void DateTimeExtensions_Between_ReturnsExpected(DateTimeExtensionInputItem inlinedData)
    {
        // Arrange
        var result = inlinedData.Input.Between(inlinedData.StartDateTime, inlinedData.EndDateTime);

        // Assert
        Assert.Equal(inlinedData.ExpectedResult, result);
    }


    public class DateTimeExtensionInputItem : IXunitSerializable
    {
        public string Description { get; set; }

        public DateTime Input { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public bool ExpectedResult { get; set; }


        public void Deserialize(IXunitSerializationInfo info)
        {
            Description = info.GetValue<string>(nameof(Description));
            Input = info.GetValue<DateTime>(nameof(Input));
            StartDateTime = info.GetValue<DateTime>(nameof(StartDateTime));
            EndDateTime = info.GetValue<DateTime>(nameof(EndDateTime));
            ExpectedResult = info.GetValue<bool>(nameof(ExpectedResult));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Description), Description);
            info.AddValue(nameof(Input), Input);
            info.AddValue(nameof(StartDateTime), StartDateTime);
            info.AddValue(nameof(EndDateTime), EndDateTime);
            info.AddValue(nameof(ExpectedResult), ExpectedResult);
        }

        public override string ToString() => $"{Input} | {StartDateTime} | {EndDateTime} | {ExpectedResult} ({Description})";
    }
}

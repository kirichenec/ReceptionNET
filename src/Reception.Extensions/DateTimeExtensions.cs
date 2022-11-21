namespace Reception.Extension
{
    public static class DateTimeExtensions
    {
        public static bool Between(this DateTime input, DateTime startDateTime, DateTime endDateTime)
        {
            return input >= startDateTime && input <= endDateTime;
        }
    }
}
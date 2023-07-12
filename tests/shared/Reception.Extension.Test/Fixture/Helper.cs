namespace Reception.Extension.Test.Fixture
{
    internal static class Helper
    {
        internal static IEnumerable<string> GetStringData(uint count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return $"value{i + 1}";
            }
        }
    }
}

namespace Reception.Extension.Test.Fixture
{
    internal static class IEnumerableExtensionsFixture
    {
        internal static IEnumerable<string> GetData(uint count)
        {
            return Helper.GetStringData(count);
        }
    }
}

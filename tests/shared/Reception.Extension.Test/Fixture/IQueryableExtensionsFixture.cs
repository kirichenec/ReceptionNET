namespace Reception.Extension.Test.Fixture
{
    internal static class IQueryableExtensionsFixture
    {
        internal static IQueryable<string> GetData(uint count)
        {
            return Helper.GetStringData(count).AsQueryable();
        }
    }
}

namespace Reception.Extension.Test
{
    public static class IQueryableExtensionsFixture
    {
        public static IQueryable<string> GetData(uint count)
        {
            var result = new string[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = $"value{i + 1}";
            }
            return result.AsQueryable();
        }
    }
}

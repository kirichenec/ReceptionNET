using System.Linq;

namespace Reception.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> value, int page, int count)
        {
            return value.Skip((page - 1) * count).Take(count);
        }
    }
}
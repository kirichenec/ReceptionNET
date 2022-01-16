using AutoMapper;
using Reception.Extension;

namespace Reception.Server.Core.Extensions
{
    public static class AutoMapperExtensions
    {
        public static TDestination Map<TSource, TDestination>(this IMapper mapper, TSource value,
            params (string, object)[] additionalMap)
        {
            return mapper.Map<TSource, TDestination>(value,
                opt => opt.AfterMap(
                    (src, dest) => additionalMap.ForEach(am =>
                    {
                        var (propertyName, value) = am;
                        var property = typeof(TDestination).GetProperty(propertyName);
                        property?.SetValue(dest, value, null);
                    })));
        }
    }
}

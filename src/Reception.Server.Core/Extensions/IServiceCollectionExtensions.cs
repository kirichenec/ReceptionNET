using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Reception.Server.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            services.Configure<T>(configuration.GetSection(typeof(T).Name));
        }
    }
}
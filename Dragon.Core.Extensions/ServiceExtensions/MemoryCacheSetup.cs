using Dragon.Core.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// Memory缓存 启动服务
    /// </summary>
    public static class MemoryCacheSetup
    {
        public static void AddMemoryCacheSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IMemoryCacheManager, MemoryCacheManager>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var value = factory.GetRequiredService<IOptions<MemoryCacheOptions>>();
                var cache = new MemoryCache(value);
                return cache;
            });
        }
    }
}

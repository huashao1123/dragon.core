using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Common
{
    public class MemoryCacheManager: IMemoryCacheManager
    {
        //引用Microsoft.Extensions.Caching.Memory;这个和.net 还是不一样，没有了Httpruntime了
        private readonly IMemoryCache _cache;
        //还是通过构造函数的方法，获取
        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        /// <param name="keys"></param>
        public void Clear(IEnumerable<string> keys)
        {
            foreach (string key in keys)
            {
                _cache.Remove(key);
            }
        }
        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            return _cache.TryGetValue(key, out string value);
        }
        /// <summary>
        /// 得到缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return _cache.Get(key);
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="CacheTime"></param>
        public void Set(string key, object value, TimeSpan CacheTime)
        {
            _cache.Set(key, value, CacheTime);
        }
    }
}

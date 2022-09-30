using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Common
{
    public interface IMemoryCacheManager
    {
        object Get(string key);
        void Set(string key, object value, TimeSpan CacheTime);
        bool Contains(string key);
        void Remove(string key);
        void Clear(IEnumerable<string> keys);
    }
}

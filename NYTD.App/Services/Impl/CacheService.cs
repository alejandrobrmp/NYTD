using NYTD.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Services.Impl
{
    public class CacheService : ICacheService
    {
        private Dictionary<string, object> Cache = new Dictionary<string, object>();
        public T Get<T>(string key, bool delete = true) where T : class
        {
            if (Cache.ContainsKey(key))
            {
                T obj = Cache[key] as T;
                if (delete)
                {
                    Cache.Remove(key);
                }
                return obj;
            }
            else
            {
                return default(T);
            }
        }

        public void Save(string key, object value)
        {
            if (Cache.ContainsKey(key))
            {
                Cache[key] = value;
            }
            else
            {
                Cache.Add(key, value);
            }
        }
    }
}

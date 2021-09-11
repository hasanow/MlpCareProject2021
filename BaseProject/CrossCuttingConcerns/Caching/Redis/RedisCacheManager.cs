
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using BaseProject.Utilities;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

namespace BaseProject.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private IDistributedCache distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public void Add(string key, object data, int duration)
        {
            distributedCache.SetString(key, data.ObjectToJsonString(), DistributedCacheEntryOptions);
            distributedCache.SetString(key + "Type", data.GetType().FullName);
        }

        public T Get<T>(string key)
        {
           return  distributedCache.GetString(key).JsonStringToObject<T>();
        }

        public object Get(string key)
        {
            string returnType = distributedCache.GetString(key + "Type");
            if (returnType != null)
                return distributedCache.GetString(key).JsonStringToObject(Type.GetType(returnType));
            return distributedCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return distributedCache.Get(key)!=null;
        }

        public void Remove(string key)
        {
            distributedCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(RedisCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(distributedCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key.ToString()).ToList();

            foreach (var key in keysToRemove)
            {
                distributedCache.Remove(key);
            }
        }

        
        private DistributedCacheEntryOptions DistributedCacheEntryOptions
        {
            get
            {
                return new DistributedCacheEntryOptions()
                {
                    SlidingExpiration=TimeSpan.FromMinutes(5),
                    AbsoluteExpirationRelativeToNow=TimeSpan.FromDays(1)
                };
            }
        }
        
    }
}

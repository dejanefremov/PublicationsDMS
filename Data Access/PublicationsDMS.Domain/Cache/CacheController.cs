using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Domain.Cache
{
    public class CacheController
    {
        public enum CacheItemKey : byte
        {
            DataItemsByParent,

            FoldersByParent,
            FolderByID,
            
            DocumentsByParent,
            DocumentByID,
            
            UserByID,
            UserByEmail
        }

        public static void AddToCache(CacheItemKey cacheKey, object cacheData)
        {
            AddToCache(cacheKey.ToString(), cacheData);
        }

        private static void AddToCache(string cacheKey, object cacheData)
        {
            MemoryCache.Default.Set(cacheKey, cacheData, new DateTimeOffset(DateTime.Now.AddDays(2)));
        }

        public static object GetFromCache(CacheItemKey cacheKey)
        {
            return GetFromCache(cacheKey.ToString());
        }

        private static object GetFromCache(string cacheKey)
        {
            return MemoryCache.Default.Get(cacheKey);
        }

        public static void ResetAllCacheItems()
        {
            foreach (CacheItemKey key in Enum.GetValues(typeof(CacheItemKey)))
            {
                ResetCacheItem(key);
            }
        }

        public static void ResetDataCacheItems()
        {
            ResetCacheItem(CacheItemKey.DataItemsByParent);
            ResetCacheItem(CacheItemKey.FoldersByParent);
            ResetCacheItem(CacheItemKey.FolderByID);
            ResetCacheItem(CacheItemKey.DocumentsByParent);
            ResetCacheItem(CacheItemKey.DocumentByID);
        }

        public static void ResetCacheItem(CacheItemKey key)
        {
            MemoryCache.Default.Remove(key.ToString());
        }
    }
}

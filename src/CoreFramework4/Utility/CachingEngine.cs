using System.Web;
using System.Web.Caching;
using System;
using CoreFramework4.Infrastructure.Services;

namespace CoreFramework4.Tool
{
    public sealed class RequestCachingEngine : ICaching
    {
        //Return null if does not exist
        public object GetItem(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        //Return null if does not exist
        public T GetItem<T>(string key) where T : class
        {
            return HttpContext.Current.Cache[key] as T;
        }

        public void SetItem(string key, object value, int defaultMinutes = 10)
        {
            if (GetItem(key) != null)
            {
                HttpContext.Current.Cache[key] = value;
            }
            else
            {
                AddItem(key, value, defaultMinutes);
            }
        }

        public object AddItem(string key, object value, int minutes)
        {
            return AddItem(key, value, DateTime.Now.AddMinutes(minutes));
        }

        public object AddItem(string key, object value, System.DateTime expiration, CacheDependency cachedependency = null)
        {
            return HttpContext.Current.Cache.Add(key, value, cachedependency, expiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public void AddItem(string key, object value, CacheDependency cachedependency = null)
        {
            HttpContext.Current.Cache.Insert(key, value, cachedependency);
        }

        public int ItemsCount
        {
            get { return HttpContext.Current.Cache.Count; }
        }

        public object RemoveItem(string key)
        {
            return HttpContext.Current.Cache.Remove(key);
        }

        public bool Exist(string key)
        {
            return GetItem(key) != null;
        }

        public static void Reset(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

    }

    public sealed class ApplicationCachingEngine : ICaching
    {
        //Return null if does not exist
        public object GetItem(string key)
        {
            return HttpRuntime.Cache[key];
        }

        //Return null if does not exist
        public T GetItem<T>(string key) where T : class
        {
            return HttpContext.Current.Cache[key] as T;
        }

        public void SetItem(string key, object value, int defaultMinutes = 10)
        {
            if (GetItem(key) != null)
            {
                HttpRuntime.Cache[key] = value;
            }
            else
            {
                AddItem(key, value, defaultMinutes);
            }
        }

        public object AddItem(string key, object value, int minutes)
        {
            return AddItem(key, value, DateTime.Now.AddMinutes(minutes));
        }

        public void AddItem(string key, object value, CacheDependency cachedependency = null)
        {
            HttpRuntime.Cache.Insert(key, value, cachedependency);
        }

        public object AddItem(string key, object value, System.DateTime expiration, CacheDependency cachedependency = null)
        {
            return HttpRuntime.Cache.Add(key, value, cachedependency, expiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public int ItemsCount
        {
            get { return HttpRuntime.Cache.Count; }
        }

        public object RemoveItem(string key)
        {
            return HttpRuntime.Cache.Remove(key);
        }

        public bool Exist(string key)
        {
            return GetItem(key) != null;
        }

        public static void Reset(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

    }

    public interface ICaching 
    {
        object GetItem(string key);
        T GetItem<T>(string key) where T : class;
        void SetItem(string key, object value, int defaultMinutes = 10);
        object AddItem(string key, object value, int minutes);
        void AddItem(string key, object value, CacheDependency cachedependency = null);
        object AddItem(string key, object value, System.DateTime expiration, CacheDependency cachedependency = null);
        int ItemsCount { get; }
        object RemoveItem(string key);
        bool Exist(string key);
    }

    public sealed class CachingFactory
    {
        public static ICaching RequestCachingEngineInstance
        {
            get
            {
                return new RequestCachingEngine();
            }
        }

        public static ICaching ApplicationCachingEngineInstance
        {
            get
            {
                return new ApplicationCachingEngine();
            }
        }

        public static ICaching CurrentInstance
        {
            get {
                return HttpContext.Current.Session["CurrentCacheEngine"] as ICaching;
            }
        }

        public static void InitializeCacheEngine(CachingOption option)
        {
            var selectedCache = (option == CachingOption.CacheOnHTTPContext) ? 
                                    RequestCachingEngineInstance : 
                                    ApplicationCachingEngineInstance;
            HttpContext.Current.Session["CurrentCacheEngine"] = selectedCache;
        }
    }
}

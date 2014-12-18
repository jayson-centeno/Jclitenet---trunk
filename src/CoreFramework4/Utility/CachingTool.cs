using System.Web;
using CoreFramework4.Infrastructure.Tools;

namespace CoreFramework4
{
    public class CachingTool : ICachingTool
    {
        public object GetItem(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public void SetItem(string key, object itemToSet)
        {
            HttpContext.Current.Cache[key] = itemToSet;
        }

        public int ItemsCount
        {
            get { return HttpContext.Current.Cache.Count; }
        }

        public object RemoveItem(string key)
        {
            return HttpContext.Current.Cache.Remove(key);
        }
    }
}

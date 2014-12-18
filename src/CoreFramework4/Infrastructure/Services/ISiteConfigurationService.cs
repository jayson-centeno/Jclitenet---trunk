using System.Collections.Generic;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Model;

namespace CoreFramework4.Implementations.Services
{
    public interface ISiteConfigurationService : IService
    {
        void CacheConfigs();
        T GetConfigValue<T>(string key);
        IEnumerable<SiteConfiguration> GetAll();
        void SaveChanges();
        void Add(SiteConfiguration model);
    }
}

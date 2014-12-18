using System.Collections.Generic;
using System.Linq;
using CoreFramework4.DataTransformation;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Tool;
using CoreFramework4.Model;

namespace CoreFramework4.Implementations.Services
{
    public class SiteConfigurationService : ISiteConfigurationService
    {
        private readonly ApplicationCachingEngine _appCacheEngine;
        private readonly ISiteConfigurationRepository _siteConfigRepository;

        public SiteConfigurationService(ApplicationCachingEngine appCacheEngine,
            ISiteConfigurationRepository siteConfigRepository)
        {
            _appCacheEngine = appCacheEngine;
            _siteConfigRepository = siteConfigRepository;
        }

        public void CacheConfigs()
        {
            var list = _siteConfigRepository.GetAll().Where(c => c.IsDeleted == false)
                                                     .AsParallel();
            var lst = list.Select(m => new SiteConfigDto()
            {
                Name = m.Name,
                ConfigValue = m.ConfigValue,
                IsDeleted = m.IsDeleted,
                IsHtml = m.IsHtml
            }).AsParallel().ToList();

            _appCacheEngine.RemoveItem(AppConstants.SITE_CONFIG_CACHE_KEY);
            _appCacheEngine.AddItem(AppConstants.SITE_CONFIG_CACHE_KEY, lst);
        }

        public T GetConfigValue<T>(string key)
        {
            var list = _appCacheEngine.GetItem<IEnumerable<SiteConfigDto>>(AppConstants.SITE_CONFIG_CACHE_KEY);

            if (list != null && list.Count() > 0)
            {
                var config = list.FirstOrDefault(c => c.Name.ToUpperInvariant() == key.ToUpperInvariant());
                if (config != null && config.ConfigValue != null && !config.ConfigValue.ToString().IsNullOrEmptyTrimmed())
                {
                    if (typeof(T) == typeof(string))
                    {
                        string value = config.ConfigValue.ToString();
                        if (config.IsHtml == true)
                        {
                            return (T)(object)value.Trim().ToHtmlDecode();
                        }
                        else
                        {
                            return (T)(object)config.ConfigValue.ToString().Trim();
                        }

                    }
                    else if (typeof(T) == typeof(bool))
                    {
                        return (T)(object)bool.Parse(config.ConfigValue.ToString().Trim());
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        return (T)(object)int.Parse(config.ConfigValue.ToString().Trim());
                    }
                }
            }
            else
            {
                CacheConfigs();
                var ret = GetConfigValue<T>(key);
                if(!EqualityComparer<T>.Default.Equals(ret, default(T)))
                {
                    return ret;
                }

            }

            return default(T);
        }

        public IEnumerable<SiteConfiguration> GetAll()
        {
            return _siteConfigRepository.GetAll()
                                        .AsParallel()
                                        .ToList();
        }

        public void SaveChanges()
        {
            _siteConfigRepository.SaveChanges();
        }

        public void Add(SiteConfiguration model)
        {
            _siteConfigRepository.Add(model);
            SaveChanges();
        }
    }
}
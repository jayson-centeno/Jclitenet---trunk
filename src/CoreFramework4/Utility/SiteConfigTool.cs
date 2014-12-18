using CoreFramework4.Implementations.Services;
namespace CoreFramework4
{
    public class SiteConfigTool
    {
        public static T GetValue<T>(string key)
        {
            return ServiceFactory.GetInstance<ISiteConfigurationService>()
                                 .GetConfigValue<T>(key);
        }

    }
}

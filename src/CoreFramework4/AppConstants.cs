namespace CoreFramework4
{
    public static class AppConstants
    {
        public const string SiteSetupModel = "SITESETUP_MODEL";
        public const string EMAILREGEX = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
        public const string GUEST_GUID = "97EDC80C-F474-40C0-8130-E240DA280CEF";
        public const string SECRET_CODE = "aaronkyle";
        public const string SITE_CONFIG_CACHE_KEY = "SITE_CONFIG_CACHE_KEY";
    }

    public static class SiteConfigName
    {
        public const string ABOUT = "about";
    }

    public enum CachingOption
    {
        CacheOnHTTPContext,
        CacheOnHTTPRuntime
    }

    public enum BlogType
    { 
        Personal = 1,
        Programming = 2,
        Others = 3
    }

    public enum TutorialCategoryEnum
    {
        ASP_WEBFORMS = 1,
        CSS3 = 2,
        JS = 3,
        jQuery = 4,
        ASP_MVC = 5,
        CS = 6,
        HTML5 = 7,       
        HTML = 8,
    }

}
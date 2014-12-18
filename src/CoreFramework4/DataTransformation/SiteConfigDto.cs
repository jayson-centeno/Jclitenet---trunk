namespace CoreFramework4.DataTransformation
{
    public class SiteConfigDto
    {
        public string Name { get; set; }
        public object ConfigValue { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsHtml { get; set; }
    }
}

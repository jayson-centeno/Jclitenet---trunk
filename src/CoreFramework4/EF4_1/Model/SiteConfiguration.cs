using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Table("SiteConfiguration")]
    public class SiteConfiguration
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string ConfigValue { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsHtml { get; set; }
    }
}

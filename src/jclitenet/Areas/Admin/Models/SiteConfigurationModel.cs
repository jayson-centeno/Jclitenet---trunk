using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jclitenet.Areas.Admin.Models
{
    public class SiteConfigurationModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [AllowHtml]
        public string ConfigValue { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsHtml { get; set; }

        public bool IsEdited { get; set; }
    }
}
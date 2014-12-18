using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreFramework4;

namespace jclitenet.Models
{
    public class TutorialModel
    {
        public TutorialModel() 
        {
            Category = new CategoryModel();
        }

        public Guid UID { get; set; }
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "*")]
        public string HtmlContent { get; set; }

        public CategoryModel Category { get; set; }

        public IEnumerable<CommentModel> Comments { get; set; }
        public int CommentCount
        {
            get;
            set;
        }

        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
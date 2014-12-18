using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Serializable]
    [Table("Tutorial")]
    public class Tutorial
    {
        public Tutorial() { }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string HtmlContent { get; set; }

        public Guid? TutorialCategory_UID { get; set; }

        [ForeignKey("TutorialCategory_UID")]
        public virtual TutorialCategory TutorialCategory { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}

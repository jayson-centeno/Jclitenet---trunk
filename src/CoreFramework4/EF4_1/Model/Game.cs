using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Table("Game")]
    public class Game
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string HtmlContent { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

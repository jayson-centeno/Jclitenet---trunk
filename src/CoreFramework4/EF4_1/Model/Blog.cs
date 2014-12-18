using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Table("Blog")]
	public class Blog
	{
        public int Id { get; set; }

        [Key]
        public Guid UID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int? Type { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }

        #region IAuditModel

        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        #endregion
    }
}

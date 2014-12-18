using System;
using System.ComponentModel.DataAnnotations;
using CoreFramework4.Model;
using CoreFramework4.Infrastructure.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Table("Album")]
    public class Album : IAuditModel
    {
        public int ID { get; set; }

        [Key]
        public Guid UID { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        #region IAuditModel

        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        #endregion
    }
}

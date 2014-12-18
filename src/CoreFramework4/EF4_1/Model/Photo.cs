using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CoreFramework4.Infrastructure.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Table("Photo")]
    public class Photo : IAuditModel
    {
        public int ID { get; set; }

        [Key]
        public Guid UID { get; set; }

        public Guid? Album_UID { get; set; }

        [ForeignKey("Album_UID")]
        public virtual Album Album { get; set; }

        public string Name { get; set; }
        public string FileName { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}

using CoreFramework4.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework4.EF4_1.Model
{
    [Table("Resume")]
    public class Resume
    {
        [Key]
        public Guid ID { get; set; }

        [Column("User_ID")]
        [ForeignKey("User")]
        public Guid UserID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual User User { get; set; }
        public virtual ResumeDetails Details { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework4.EF4_1.Model
{
    [Table("Resume_Details")]
    public class ResumeDetails
    {
        [Key, ForeignKey("Resume")]
        public Guid ID { get; set; }

        public virtual Resume Resume { get; set; }

        public string Introduction { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string HomePhone { get; set; }

        public string Skills { get; set; }

        public string Interests { get; set; }
    }
}

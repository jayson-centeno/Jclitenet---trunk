using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Table("ChangeLog")]
    public class ChangeLog
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jclitenet.Models
{
    public class PhotoModel
    {
        public int ID { get; set; }
        public Guid UID { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
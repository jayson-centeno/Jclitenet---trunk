using System;
using System.Collections.Generic;

namespace jclitenet.Models
{
    public class AlbumModel
    {
        public int ID { get; set; }
        public Guid UID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }
        public IEnumerable<PhotoModel> Photos { get; set; }
    }
}
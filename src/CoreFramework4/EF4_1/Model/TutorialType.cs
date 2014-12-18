using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Serializable]
    [Table("TutorialType")]
    public class TutorialType
    {
        public TutorialType() { }

        [Key]
        public Guid UID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TutorialCategory> TutorialCategory { get; set; }
    }
}

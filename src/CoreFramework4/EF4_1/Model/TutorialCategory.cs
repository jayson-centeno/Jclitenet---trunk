using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Serializable]
    [Table("TutorialCategory")]
    public class TutorialCategory
    {
        public TutorialCategory() { }

        [Key]
        public Guid UID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual TutorialType TutorialType { get; set; }
        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}
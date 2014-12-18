using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jclitenet.Models
{
    public class TutorialCategoryModel
    {
        public TutorialCategoryModel() { }

        public Guid UID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jclitenet.Models
{
    public class TutorialModelList
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<TutorialModel> TutorialModels { get; set; }
    }
}
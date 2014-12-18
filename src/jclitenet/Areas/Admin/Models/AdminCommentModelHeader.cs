using System.Collections.Generic;

namespace jclitenet.Areas.Admin.Models
{
    public class AdminCommentModelHeader
    {
        public IEnumerable<AdminCommentModel> Comments { get; set; }

        public string SelectedTutorial { get; set; }
        public IEnumerable<AdminTutorialModel> Tutorials { get; set; }
    }
}
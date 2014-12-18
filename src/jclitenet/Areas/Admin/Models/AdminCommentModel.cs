using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jclitenet.Areas.Admin.Models
{
    public class AdminCommentModel
    {
        public int Id { get; set; }

        public string CommentText { get; set; }

        public string AnonymousName { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public int? Tutorial_ID { get; set; }

        public string IpAddress { get; set; }

        public bool IsApproved { get; set; }

        public bool IsValidWebSite { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
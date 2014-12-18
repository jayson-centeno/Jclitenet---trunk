using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jclitenet.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please input a comment")]
        public string CommentText { get; set; }

        [Required(ErrorMessage = "Please input your name")]
        public string AnonymousName { get; set; }

        [Required(ErrorMessage = "Please input your email")]
        public string Email { get; set; }

        public string Website { get; set; }

        public string Title { get; set; }
        public int? Tutorial_ID { get; set; }

        public int? Game_ID { get; set; }

        public bool IsApproved { get; set; }
        public bool IsValidWebSite { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
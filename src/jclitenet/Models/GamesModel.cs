using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace jclitenet.Models
{
    public class GamesModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "*")]
        public string HtmlContent { get; set; }

        public IEnumerable<CommentModel> Comments { get; set; }
    }
}
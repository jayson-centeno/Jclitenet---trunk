using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jclitenet.Models
{
    public class SideMenuModelItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Posted { get; set; }
        public string Href { get; set; }
        public string Content { get; set; }
        public int CommentCount { get; set; }

        public string Name { get; set; }
        public string Website { get; set; }
    }
}
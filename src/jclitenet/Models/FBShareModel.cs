using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jclitenet.Models
{
    public class FBShareModel
    {
        public FBShareModel() { }

        public FBShareModel(string id, string link)
        {
            Id = id;
            Link = link;
        }

        public string Id { get; set; }
        public string Link { get; set; }
    }
}
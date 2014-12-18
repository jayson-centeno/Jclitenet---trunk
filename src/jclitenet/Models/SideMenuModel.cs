using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jclitenet.Models
{
    public class SideMenuModel
    {
        public string Title { get; set; }
        public IEnumerable<SideMenuModelItem> SideMenuItems { get; set; }
    }
}
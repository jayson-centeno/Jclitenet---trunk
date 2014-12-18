using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreFramework4.Migration.Migration
{
    public class AppVersionItem
    {
        public string Name;
        public Guid Guid;
        public int CurrentVersion;
        public int LatestVersion;
        public Type Container;
    }
}
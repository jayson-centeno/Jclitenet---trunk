using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace CoreFramework4.Implementations.Security
{
    [Serializable]
    public class SiteIdentity : GenericIdentity
    {
        public SiteIdentity(string username) : base(username) { }
        public SiteIdentity(string username, string type) : base(username, type) { }
        public SiteIdentity(string username, string fullename, string type): base(username, type) 
        {
            FullName = fullename;
        }

        public string Email { get; set; }
        public string FullName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using CoreFramework4.Model;

namespace CoreFramework4.Implementations.Security
{
    [Serializable]
    public class SitePrincipal: IPrincipal
    {
        private LogonInfo _user;

        public SitePrincipal(LogonInfo user) 
        {
            _user = user;
        }

        public IIdentity Identity
        {
            get { return _user; }
        }

        public bool IsInRole(string role)
        {
           return true;
        }



    }
}

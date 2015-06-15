using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using CoreFramework4.Infrastructure.Security;

namespace CoreFramework4.Implementations.Security
{
    [Serializable]
    public class LogonInfo : MarshalByRefObject, ILogonInfo, IIdentity
    {
        public Guid Id { get; set; }
        public DateTime? DateLogin { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsGuest { get; set; }

        #region IIdentity

        public string AuthenticationType 
        {
            get { return "Forms"; }
        }

        public bool IsAuthenticated
        {
            get { return (Id != null); }
        }

        public string Name
        {
            get 
            {
                return this.Name;
            }
        }

        #endregion

    }
}

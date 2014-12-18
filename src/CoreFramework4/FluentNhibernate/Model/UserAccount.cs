using System.Collections.Generic;

namespace CoreFramework4.FluentNHibernate.Model
{
    public class UserAccount
    {
        public UserAccount()
        {
            LogInInfos = new List<LogInInfo>();
        }

        public virtual int ID { get; private set; }
        public virtual string UserCode { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual int? Status { get; set; }

        public virtual IList<LogInInfo> LogInInfos { get; private set; }

        public virtual void AddLoginInfo(LogInInfo logInInfo)
        {
            logInInfo.UserAccount = this;
            LogInInfos.Add(logInInfo);
        }

    }
}

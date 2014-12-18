using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreFramework4.FluentNHibernate.Model
{
    public class LogInInfo
    {
        public virtual int ID { get; private set; }
        public virtual DateTime? DateLogIn { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}

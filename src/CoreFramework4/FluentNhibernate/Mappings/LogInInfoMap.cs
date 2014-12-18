using CoreFramework4.FluentNHibernate.Model;
using FluentNHibernate.Mapping;

namespace CoreFramework4.FluentNhibernate.Mappings
{
    public sealed class LogInInfoMap : ClassMap<LogInInfo>
    {
        public LogInInfoMap()
        {
            Id(x => x.ID);
            References(x => x.UserAccount);
        }
    }
}
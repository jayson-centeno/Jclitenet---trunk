using CoreFramework4.FluentNHibernate.Model;
using FluentNHibernate.Mapping;

namespace CoreFramework4.FluentNHibernate.Mappings
{
    public sealed class UserAccountMap : ClassMap<UserAccount>
    {
        public UserAccountMap()
        {
            Id(x => x.ID);
            Map(x => x.UserCode);
            Map(x => x.UserName);
            Map(x => x.Password);
            Map(x => x.Status);
            HasMany(x => x.LogInInfos)
                .Inverse()
                .Cascade.All();
        }
    }
}

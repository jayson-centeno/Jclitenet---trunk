// Type: FluentNHibernate.Cfg.Db.MsSqlConfiguration
// Assembly: FluentNHibernate, Version=1.2.0.712, Culture=neutral, PublicKeyToken=8aa435e3cb308880
// Assembly location: D:\Jayson\Projects\CoreFramework4\dll\FluentNHibernate.dll

namespace FluentNHibernate.Cfg.Db
{
    public class MsSqlConfiguration : PersistenceConfiguration<MsSqlConfiguration, MsSqlConnectionStringBuilder>
    {
        protected MsSqlConfiguration();
        public static MsSqlConfiguration MsSql7 { get; }
        public static MsSqlConfiguration MsSql2000 { get; }
        public static MsSqlConfiguration MsSql2005 { get; }
        public static MsSqlConfiguration MsSql2008 { get; }
    }
}

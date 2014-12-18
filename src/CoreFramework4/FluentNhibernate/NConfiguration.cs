using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace CoreFramework4.FluentNhibernate
{
    public class NConfiguration
    {
        public static ISessionFactory CreateSessionFactoryForMsSql2005()
        {
            return Fluently
                    .Configure()
                    .Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnectionString")))
                    .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                    .BuildSessionFactory();
        }

        public static ISessionFactory CreateSessionFactoryForMsSql2008()
        {
            return Fluently
                    .Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnectionString")))
                    .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                    .BuildSessionFactory();
        }
    }
}

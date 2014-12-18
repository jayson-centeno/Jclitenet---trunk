using System.Configuration;
using StructureMap.Configuration.DSL;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Implementations.Repository;
using CoreFramework4.Tool;

namespace CoreFramework4
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry() : this(false) { }

        public DomainRegistry(bool excludeRepository)
        {
            RegisterDataContextToBeCachedPerRequest();
            //WireUpRepositories();
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
        }

        private void RegisterDataContextToBeCachedPerRequest()
        {
            For<DbManager>()
                .HybridHttpOrThreadLocalScoped()
                .Use(ctx => new DbManager(GetConnectionString(), false));
        }

        private void WireUpRepositories()
        {
            // NOTE:
            // This won't work, since our individual implementatons
            // are more tied-up to more detailed implementations per IRepository derivative
            // not just the one with the generics
            // thus we will need to them manually

            For<RequestCachingEngine>().Use<RequestCachingEngine>();
            For<ApplicationCachingEngine>().Use<ApplicationCachingEngine>();
            //For<IUserRepository>().Use<UserRepository>();
            //For<IAlbumRepository>().Use<AlbumRepository>();
            //For<ITutorialRepository>().Use<TutorialRepository>();
            
        }
    }
}

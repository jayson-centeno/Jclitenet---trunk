using StructureMap;
using System.Configuration;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Implementations.Repository;
using CoreFramework4.Infrastructure.Services;

namespace CoreFramework4
{
    public class BootStrapper
    {
        public static void Bootstrap()
        {
            ObjectFactory.Configure(config =>
            {
                config.Scan(scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AddAllTypesOf(typeof(IRepository<>));
                    scanner.AddAllTypesOf<IService>();
                    scanner.WithDefaultConventions();
                });
            });

            ObjectFactory.Configure(cfg => cfg.AddRegistry(new DomainRegistry()));
           // ObjectFactory.Configure(cfg => cfg.AddRegistry(new ServiceRegistry()));
        }
    }
}
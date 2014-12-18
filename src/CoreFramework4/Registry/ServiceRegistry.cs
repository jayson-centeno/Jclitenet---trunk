using CoreFramework4.Implementations.Repository;
using StructureMap.Configuration.DSL;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Implementations.Services;
using CoreFramework4.Infrastructure.Services;

namespace CoreFramework4
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            //For<IAuthenticationService>().Use<AuthenticationService>();
            //For<IAlbumService>().Use<AlbumService>();
            //For<SystemUserPreferenceRepository>().Use<SystemUserPreferenceRepository>();
        }
    }
}

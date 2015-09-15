using System.Web.Http.Controllers;
using StructureMap.Configuration.DSL;

namespace CoreFramework4
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            Scan(p =>
            {
                p.TheCallingAssembly();
                p.AddAllTypesOf<IHttpController>().NameBy(x => x.Name);
            });
        }
    }
}

using System;
using StructureMap;
using CoreFramework4.Infrastructure;
using System.Collections;

namespace CoreFramework4
{
    public class StructureMapServiceLocator : IServiceLocator
    {
        //public void Configure()
        //{
        //    // ObjectFactory.Configure(cfg => cfg.AddRegistry(new DomainRegistry()));
        //}

        public T GetInstance<T>()
        {
            return ObjectFactory.TryGetInstance<T>();
        }

        public void Inject<T>(object plugin)
        {
            ObjectFactory.Inject(typeof(T), plugin);
        }

        public object GetInstance(Type type)
        {
            return ObjectFactory.TryGetInstance(type);
        }

        public void Reset()
        {
            ObjectFactory.ResetDefaults();
        }

        public IList GetInstances(Type type)
        {
            return ObjectFactory.GetAllInstances(type);
        }
    }
}

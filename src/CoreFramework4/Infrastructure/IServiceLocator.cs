using System;
using System.Collections;

namespace CoreFramework4.Infrastructure
{
    public interface IServiceLocator
    {
        //void Configure();
        T GetInstance<T>();
        void Inject<T>(object plugin);
        object GetInstance(Type type);
        IList GetInstances(Type type);
        void Reset();
    }
}

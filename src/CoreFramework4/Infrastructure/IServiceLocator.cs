using System;

namespace CoreFramework4.Infrastructure
{
    public interface IServiceLocator
    {
        //void Configure();
        T GetInstance<T>();
        void Inject<T>(object plugin);
        object GetInstance(Type type);
        void Reset();
    }
}

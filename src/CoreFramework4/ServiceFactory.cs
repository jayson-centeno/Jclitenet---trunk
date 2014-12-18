using System;
using CoreFramework4.Infrastructure;

namespace CoreFramework4
{
    public class ServiceFactory
    {
        static IServiceLocator _instance = null;

        static ServiceFactory()
        {
            _instance = new StructureMapServiceLocator();
        }

        public static void SetLocator(IServiceLocator instance)
        {
            _instance = instance;
        }

        public static void Inject<T>(T plugin)
        {
            _instance.Inject<T>(plugin);
        }

        public static object GetInstance(Type type)
        {
            return _instance.GetInstance(type);
        }

        public static T GetInstance<T>()
        {
            return _instance.GetInstance<T>();
        }

        public static void Reset()
        {
            _instance.Reset();
        }
    }
}

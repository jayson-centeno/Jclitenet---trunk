using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace CoreFramework4.Migration.RefectionExtension
{
    public static class Reflector
    {
        /// <summary>
        /// Loads all application assemblies into the current domain and returns all assemblies of the domain
        /// </summary>
        public static IEnumerable<Assembly> GetAllApplicationAssemblies()
        {
            LoadAllApplicationAssemblies();
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public static void FindTypes<TContainer, TFind>(Action<FieldInfo, TFind> foundAction) where TFind : class
        {
            FindTypes<TContainer, TFind>(null, foundAction);
        }

        public static void FindTypes<TContainer, TFind>(Func<Type, IEnumerable<FieldInfo>> customResolver, Action<FieldInfo, TFind> foundAction) where TFind : class
        {
            foreach (var assembly in Reflector.GetAllApplicationAssemblies())
            {
                if (Reflector.IsFrameworkAssembly(assembly)) continue;

                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(TContainer), false).Length > 0)
                    {
                        if (!(type.IsSealed && type.IsAbstract)) throw new Exception(type.FullName + " must be declared static to use TranslationKeysContainer");
                        FindTypesRecursive(type, customResolver, foundAction);
                    }
                }
            }
        }

        static void FindTypesRecursive<TFind>(Type type, Func<Type, IEnumerable<FieldInfo>> customResolver, Action<FieldInfo, TFind> foundAction) where TFind : class
        {
            var targetType = typeof(TFind);
            IEnumerable<FieldInfo> foundFields = null;

            if (customResolver != null)
            {
                foundFields = customResolver(type);
            }
            else
            {
                foundFields = type.GetFields()
                                  .Where(field => field.FieldType == targetType);
            }

            foreach (var field in foundFields)
            {
                var instance = field.GetValue(null) as TFind;
                foundAction(field, instance);
            }

            foreach (var nestedType in type.GetNestedTypes()) FindTypesRecursive(nestedType, customResolver, foundAction);
        }

        /// <summary>
        /// Load all application assemblies into the current domain, ensuring that all assemblies have been loaded when needed for reflection.
        /// </summary>
        public static void LoadAllApplicationAssemblies()
        {
            string path = AppDomain.CurrentDomain.RelativeSearchPath;
            string[] files = System.IO.Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);

            foreach (var filepath in files)
            {
                // Load the file into the application domain.
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(filepath);
                Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName);
            }
        }

        /// <summary>
        /// Checks if the assembly path contains "Windows" to avoid enumerating over irrelevant .NET assemblies when using reflection
        /// </summary>
        public static bool IsFrameworkAssembly(Assembly assembly)
        {
            try
            {
                string codeBaseLocation = assembly.CodeBase;
                if (codeBaseLocation.Contains(@"/Windows/")) return true;
            }
            catch { }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreFramework4.Migration.RefectionExtension;

namespace CoreFramework4.Migration.Migration
{
    public static class AppVersion
    {
        internal static List<Type> _AppVersionRepositories = null;

        public static List<AppVersionItem> GetCurrentVersions()
        {
            if (_AppVersionRepositories == null)
            {
                //NOTE: Application assemblies of the website may unload during inactivity and cause repositories to disappear
                _AppVersionRepositories = new List<Type>();

                var assemblies = Reflector.GetAllApplicationAssemblies()
                                          .Where(a => !Reflector.IsFrameworkAssembly(a))
                                          .AsParallel();

                foreach (var assembly in assemblies)
                {
                    var appVersion = assembly.GetTypes()
                                             .Where(a => a.GetCustomAttributes().Any(att => att.GetType() == typeof(AppVersionContainer)))
                                             .AsParallel();

                    foreach (var type in appVersion)
                    {
                        if (!(type.IsSealed && type.IsAbstract)) throw new Exception(type.FullName + " must be declared static to use AppVersionContainer");

                        _AppVersionRepositories.Add(type);
                    }
                }
            }

            var list = new List<AppVersionItem>();

            foreach (var type in _AppVersionRepositories)
            {
                object[] attributes = type.GetCustomAttributes(typeof(AppVersionContainer), false);
                if (attributes.Length != 1) throw new Exception(type.FullName + " has zero or more than one AppVersionContainer");

                var attribute = attributes[0] as AppVersionContainer;

                //Check for duplicate guid
                if (list.Any(a => a.Guid == attribute.ContainerGuid)) throw new Exception("ContainerGuid (" + attribute.ContainerGuid + ") is already in use. Check that you are not using the same ContainerGuid for a different AppVersionContainer.");

                //Current version
                int currentVersion = GetCurrentVersion(attribute.ContainerGuid);

                //Available version
                int latestVersion = GetLatestVersion(type);

                list.Add(new AppVersionItem()
                {
                    Name = attribute.ContainerName,
                    Guid = attribute.ContainerGuid,
                    CurrentVersion = currentVersion,
                    LatestVersion = latestVersion,
                    Container = type
                });
            }

            return list;
        }

        public static int GetLatestVersion(Type container)
        {
            var updateMethods = GetUpdateMethods(container);
            if (updateMethods.Count == 0) return 0;
            int latestVersion = GetMethodVersion(updateMethods.OrderBy(m => m.Name).Last());
            return latestVersion;
        }

        public static int GetCurrentVersion(Guid containerGuid)
        {
            string value = VariableStore.Get("AppVersion_" + containerGuid.ToString("N")) ?? "0";
            return int.Parse(value);
        }

        public static void SetCurrentVersion(Guid containerGuid, int version)
        {
            VariableStore.Set("AppVersion_" + containerGuid.ToString("N"), version.ToString());
        }

        private static int GetMethodVersion(MethodInfo method)
        {
            try
            {
                return int.Parse(method.Name.Split('_')[1]);
            }
            catch
            {
                throw new Exception("AppVersionContainer method name was invalid: " + method.Name);
            }
        }

        private static List<MethodInfo> GetUpdateMethods(Type container)
        {
            var updateMethods = new List<MethodInfo>();

            #region Find methods with version number higher than current version
            foreach (var method in container.GetMethods(BindingFlags.NonPublic | BindingFlags.Static).OrderBy(m => m.Name))
            {
                if (!method.Name.StartsWith("Update_")) throw new Exception("Illegal method name: " + method.Name + ". AppVersionContainer can only contain update methods following the naming scheme 'Update_XXXX'.");
                updateMethods.Add(method);
            }
            #endregion

            return updateMethods;
        }

        public static void UpdateAll()
        {
            var versions = AppVersion.GetCurrentVersions();
            foreach (var version in versions) 
            {
                if (version.CurrentVersion < version.LatestVersion) 
                {
                    AppVersion.UpdateToLatestVersion(version.Guid);
                }
            } 
        }

        public static void UpdateToLatestVersion(Guid containerGuid)
        {
            var version = GetCurrentVersions().Single(cv => cv.Guid == containerGuid);
            if (version.CurrentVersion < version.LatestVersion)
            {
                //Update to latest version
                UpdateToVersion(containerGuid, version.LatestVersion);
            } 
        }

        public static void UpdateToVersion(Guid containerGuid, int toVersion)
        {
            var version = GetCurrentVersions().Single(cv => cv.Guid == containerGuid);
            var updateMethods = GetUpdateMethods(version.Container);

            #region Update
            int currentVersion = version.CurrentVersion;
            foreach (var updateMethod in updateMethods)
            {
                int methodVersion = int.Parse(updateMethod.Name.Split('_')[1]);

                if (methodVersion <= currentVersion) continue;
                if (methodVersion > toVersion) continue;

                if (currentVersion < methodVersion)
                {
                    try
                    {
                        updateMethod.Invoke(null, null);
                    }
                    catch (Exception ex)
                    {
                        ex = ex.InnerException;
                        //RHF.ErrorReporting.ErrorHandler.HandleError(ex); //required: error reporting may not be loaded normally if script fails in initialization
                        throw new Exception("AppVersion update script failed [" + version.Name + ", upgrading to " + methodVersion + "]. Error: " + ex.Message);
                    }
                    currentVersion = methodVersion;
                    SetCurrentVersion(version.Guid, methodVersion);
                }
                else throw new Exception("AppVersion update script was older than current version.");
            }
            #endregion

            //RHR.Log("AppVersion", string.Format("Updated [{0}] from version {1} to {2}", version.Name, version.CurrentVersion, currentVersion), null, RHR.LogSeverity.Information);
        }
    }
}
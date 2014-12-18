using System;

namespace CoreFramework4.Migration.Migration
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AppVersionContainer : Attribute
    {
        public AppVersionContainer(string containerName, string containerGuid)
        {
            ContainerName = containerName;
            ContainerGuid = new Guid(containerGuid);
        }

        public string ContainerName { get; set; }
        public Guid ContainerGuid { get; set; }
    }
}
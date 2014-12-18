namespace CoreFramework4.Infrastructure.Tools
{
    public interface ICachingTool
    {
        object GetItem(string key);
        void SetItem(string key, object itemToSet);
        int ItemsCount { get; }
        object RemoveItem(string key);
    }
}

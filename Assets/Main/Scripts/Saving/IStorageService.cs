namespace Main.Scripts.Saving
{
    public interface IStorageService
    {
        void Save<T>(string key, T data);
        T Load<T>(string key, T defaultValue);
        bool HasKey(string key);
        void DeleteKey(string key);
        
    }
}
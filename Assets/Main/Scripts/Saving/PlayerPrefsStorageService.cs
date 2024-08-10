using UnityEngine;

namespace Main.Scripts.Saving
{
    public class PlayerPrefsStorageService : IStorageService
    {
        public void Save<T>(string key, T data)
        {
            string jsonData = JsonUtility.ToJson(new Wrapper<T>(data));
            PlayerPrefs.SetString(key, jsonData);
            PlayerPrefs.Save();
        }

        public T Load<T>(string key, T defaultValue)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }

            string jsonData = PlayerPrefs.GetString(key);
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(jsonData);
            return wrapper.Value;
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
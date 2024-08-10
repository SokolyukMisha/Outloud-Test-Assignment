using System;
using System.Text;
using UnityEngine;

namespace Main.Scripts.Saving
{
    public class Base64StorageService : IStorageService
    {
        public void Save<T>(string key, T data)
        {
            string jsonData = JsonUtility.ToJson(new Wrapper<T>(data));
            string base64Data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonData));
            PlayerPrefs.SetString(key, base64Data);
            PlayerPrefs.Save();
        }

        public T Load<T>(string key, T defaultValue)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }

            string base64Data = PlayerPrefs.GetString(key);
            string jsonData = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
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
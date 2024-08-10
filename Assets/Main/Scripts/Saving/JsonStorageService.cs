using System.IO;
using UnityEngine;

namespace Main.Scripts.Saving
{
    public class JsonFileStorageService : IStorageService
    {
        private readonly string _directoryPath = "/tmp/saves/";

        public JsonFileStorageService()
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }

        public void Save<T>(string key, T data)
        {
            string filePath = GetFilePath(key);
            string jsonData = JsonUtility.ToJson(new Wrapper<T>(data));
            File.WriteAllText(filePath, jsonData);
        }

        public T Load<T>(string key, T defaultValue)
        {
            string filePath = GetFilePath(key);
            if (!File.Exists(filePath))
            {
                return defaultValue;
            }

            string jsonData = File.ReadAllText(filePath);
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(jsonData);
            return wrapper.Value;
        }

        public bool HasKey(string key)
        {
            string filePath = GetFilePath(key);
            return File.Exists(filePath);
        }

        public void DeleteKey(string key)
        {
            string filePath = GetFilePath(key);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GetFilePath(string key)
        {
            return Path.Combine(_directoryPath, $"{key}.json");
        }
    }

}
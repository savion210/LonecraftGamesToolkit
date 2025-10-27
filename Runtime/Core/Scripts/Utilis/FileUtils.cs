using System;
using System.IO;
using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class FileUtils
    {
        public static void EnsureDirectoryForFile(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static void WriteJsonToFile<T>(T data, string path, bool pretty = true)
        {
            EnsureDirectoryForFile(path);
            File.WriteAllText(path, JsonUtility.ToJson(data, pretty));
        }

        public static bool TryReadJsonFromFile<T>(string path, out T result) where T : class
        {
            result = null;
            if (!File.Exists(path)) return false;
            try
            {
                var json = File.ReadAllText(path);
                result = JsonUtility.FromJson<T>(json);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[FileUtils] Failed to read JSON: {e.Message}");
                return false;
            }
        }
    }
}
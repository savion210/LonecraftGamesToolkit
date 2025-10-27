using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class PlayerPrefsUtils
    {
        public static void SetBool(string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);

        public static bool GetBool(string key, bool defaultValue = false)
            => PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;

        public static void SetJson<T>(string key, T value)
            => PlayerPrefs.SetString(key, JsonUtility.ToJson(value));

        public static T GetJson<T>(string key, T defaultValue = default)
        {
            var json = PlayerPrefs.GetString(key, "");
            if (string.IsNullOrEmpty(json)) return defaultValue;
            return JsonUtility.FromJson<T>(json);
        }
    }
}
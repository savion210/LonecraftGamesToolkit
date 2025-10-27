#if UNITY_EDITOR
using UnityEditor;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class EditorUtils
    {
        public static void Ping(UnityEngine.Object obj)
        {
            if (!obj) return;
            EditorGUIUtility.PingObject(obj);
            Selection.activeObject = obj;
        }

        public static void CreateFolderIfMissing(string path)
        {
            var parts = path.Trim('/').Split('/');
            var current = "Assets";
            foreach (var p in parts)
            {
                var next = $"{current}/{p}";
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, p);
                current = next;
            }
        }

        public static void MarkDirty(UnityEngine.Object obj)
        {
            if (obj) EditorUtility.SetDirty(obj);
        }
    }
}
#endif
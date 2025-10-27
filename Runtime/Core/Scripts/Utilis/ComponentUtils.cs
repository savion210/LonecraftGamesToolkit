using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class ComponentUtils
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (!gameObject) return null;
            if (gameObject.TryGetComponent<T>(out var component)) return component;
            return gameObject.AddComponent<T>();
        }

        public static bool EnsureComponent<T>(this GameObject go, out T component) where T : Component
        {
            if (!go)
            {
                component = null;
                return false;
            }

            if (!go.TryGetComponent(out component))
            {
                component = go.AddComponent<T>();
                return false;
            }

            return true;
        }

        public static void DestroyComponentIfExists<T>(this GameObject go) where T : Component
        {
            if (!go) return;
            if (go.TryGetComponent<T>(out var c))
                Object.Destroy(c);
        }
    }
}
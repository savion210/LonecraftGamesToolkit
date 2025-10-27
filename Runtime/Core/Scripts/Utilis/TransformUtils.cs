using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class TransformUtils
    {
        public static void DestroyChildren(this Transform parent)
        {
            if (!parent) return;
            for (int i = parent.childCount - 1; i >= 0; i--)
                Object.Destroy(parent.GetChild(i).gameObject);
        }

        public static Transform FindDeepChild(this Transform parent, string name)
        {
            if (!parent) return null;
            foreach (Transform child in parent)
            {
                if (child.name == name) return child;
                var result = child.FindDeepChild(name);
                if (result) return result;
            }

            return null;
        }

        public static Transform GetOrAddChild(this Transform parent, string name)
        {
            var existing = parent.Find(name);
            if (existing) return existing;
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            return go.transform;
        }
        
        public static void ResetTransform(this Transform t)
        {
            if (!t) return;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }
        
        public static void SetLayer(this Transform t, int layer, bool includeChildren)
        {
            if (!t) return;
            t.gameObject.layer = layer;
            if (!includeChildren) return;
            foreach (Transform child in t)
                child.SetLayer(layer, true);
        }
        
        public static void SetActiveSafe(this Transform t, bool state)
        {
            if (t && t.gameObject.activeSelf != state) t.gameObject.SetActive(state);
        }
        
    }
}
using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class GameObjectUtils
    {
        public static void SetActiveSafe(this GameObject go, bool state)
        {
            if (go && go.activeSelf != state) go.SetActive(state);
        }

        public static void SetLayer(this GameObject go, int layer, bool includeChildren)
        {
            if (!go) return;
            go.layer = layer;
            if (!includeChildren) return;
            foreach (Transform child in go.transform)
                child.gameObject.SetLayer(layer, true);
        }

        public static void DontDestroyOnLoadSafe(this GameObject go)
        {
            if (go) Object.DontDestroyOnLoad(go);
        }
    }
}
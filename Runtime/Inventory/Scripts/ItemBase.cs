using System;
using UnityEngine;

namespace LonecraftGames.Toolkit.Inventory
{
    [Serializable]
    public abstract class ItemBase
    {
        public string Id;
        public string Name;
        public Sprite Icon;
        public GameObject Prefab;
        public int MaxStackSize = 1;

        protected ItemBase(string id, string name, Sprite icon, GameObject prefab, int maxStack = 1)
        {
            Id = id; Name = name; Icon = icon; Prefab = prefab;
            MaxStackSize = Mathf.Max(1, maxStack);
        }

        public virtual bool Use(GameObject user) => false; // override per type
        public virtual ItemBase Clone() => (ItemBase)MemberwiseClone();
    }
}
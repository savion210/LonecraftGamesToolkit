using System;
using System.Collections.Generic;
using UnityEngine;

namespace LonecraftGames.Toolkit.Inventory
{
    [CreateAssetMenu(fileName = "ItemCatalog", menuName = "LonecraftGames/Toolkit/Inventory/ItemCatalog", order = 1)]
    public class ItemCatalog : ScriptableObject
    {
        [Serializable]
        public class ConsumableDef
        {
            public string Id;
            public string Name;
            public Sprite Icon;
            public GameObject Prefab;
            public int MaxStack = 1;
            public int HealAmount = 0;
        }

        [Serializable]
        public class EquipmentDef
        {
            public string Id;
            public string Name;
            public Sprite Icon;
            public GameObject Prefab;
            public int MaxStack = 1;
            public int AttackBonus = 0;
        }

        public List<ConsumableDef> Consumables = new();
        public List<EquipmentDef> Equipments = new();

        Dictionary<string, ItemBase> _cache;

        public void Build()
        {
            _cache = new Dictionary<string, ItemBase>(StringComparer.Ordinal);
            foreach (var c in Consumables)
                _cache[c.Id] = new ConsumableItem(c.Id, c.Name, c.Icon, c.Prefab, c.MaxStack, c.HealAmount);
            foreach (var e in Equipments)
                _cache[e.Id] = new EquipmentItem(e.Id, e.Name, e.Icon, e.Prefab, e.MaxStack, e.AttackBonus);
        }

        public ItemBase Resolve(string id)
        {
            if (_cache == null) Build();
            return _cache != null && _cache.TryGetValue(id, out var item) ? item : null;
        }
    }
}
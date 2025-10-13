using UnityEngine;

namespace LonecraftGames.Toolkit.Inventory
{
    [System.Serializable]
    public class EquipmentItem : ItemBase
    {
        public int AttackBonus;

        public EquipmentItem(string id, string name, Sprite icon, GameObject prefab, int maxStack, int atk)
            : base(id, name, icon, prefab, maxStack) => AttackBonus = atk;

        public override bool Use(GameObject user)
        {
            Debug.Log($"Equipping {Name} to {user.name}, Attack Bonus: {AttackBonus}");
            return false; // not consumed by default
        }
    }
}
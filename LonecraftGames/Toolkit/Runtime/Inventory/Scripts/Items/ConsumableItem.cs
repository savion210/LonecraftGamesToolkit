using LonecraftGames.Toolkit.Gameplay.HealthPro;
using UnityEngine;

namespace LonecraftGames.Toolkit.Inventory
{
    [System.Serializable]
    public class ConsumableItem : ItemBase
    {
        public int HealAmount;

        public ConsumableItem(string id, string name, Sprite icon, GameObject prefab, int maxStack, int heal)
            : base(id, name, icon, prefab, maxStack) => HealAmount = heal;

        public override bool Use(GameObject user)
        {
            Health health = user.GetComponent<Health>();
            if (health == null || !health.CanHeal())
            {
                return false;
            }
            else
            {
                health.Heal(HealAmount);
                return true;
            }
        }
    }
}
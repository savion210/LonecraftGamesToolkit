using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    public class HealEventArgs
    {
        public MonoBehaviour Healer { get; }
        public IHealth Target { get; }
        public int Heal { get; }

        public HealType Type { get; }

        public HealEventArgs(MonoBehaviour healer, IHealth target, int heal, HealType type)
        {
            Healer = healer;
            Target = target;
            Heal = heal;
            Type = type;
        }
    }
}
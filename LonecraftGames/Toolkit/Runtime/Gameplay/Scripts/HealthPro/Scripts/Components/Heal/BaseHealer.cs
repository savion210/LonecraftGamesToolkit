using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    [AddComponentMenu("LonecraftGames/HealthPro/BaseHealer")]
    /// <summary>
    ///  Base class for all healers.
    /// </summary>
    public abstract class BaseHealer : MonoBehaviour, IHealer
    {
        public int HealAmount => healthAmount;
        public HealType Type => healType;
        public OnHealEventChannel onHealEventChannel;
        
        [SerializeField] protected int healthAmount = 10;
        [SerializeField] protected HealType healType = HealType.Heal;

        /// <summary>
        ///  Deal damage to the target.
        /// </summary>
        /// <param name="target">The target to deal damage to.</param>
        public abstract void Heal(IHealth target);
        
        /// <summary>
        ///  Raise the damage event.
        /// </summary>
        /// <param name="target"> The target to give health to.</param>
        /// <param name="heal"> The amount of health to give.</param>
        protected void RaiseHealEvent(IHealth target, int heal)
        {
            onHealEventChannel.Raise(new HealEventArgs(this, target, heal, healType));
        }
    }
}
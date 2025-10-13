using UnityEngine;

namespace LonecraftGames.Toolkit.Gameplay.HealthPro
{
    /// <summary>
    ///  Represents the health of an entity.
    /// </summary>
    [AddComponentMenu("LonecraftGames/HealthPro/Health")]
    [DisallowMultipleComponent]
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private OnHealthChangedEventChannel onHealthChangedEvent;
        [SerializeField] private OnDeathEventChannel onDeathEvent;
        [SerializeField] private DamageResistance[] resistances;

        public int _currentHealth;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => _currentHealth;
        public bool IsDead => _currentHealth <= 0;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        /// <summary>
        ///  Deals damage to the entity.
        /// </summary>
        /// <param name="damage"> The amount of damage to deal. </param>
        /// <param name="type"> The type of damage to deal. </param>
        public void TakeDamage(int damage, DamageType type)
        {
            if (IsDead) return;

            float resistance = GetResistance(type);
            int finalDamage = Mathf.CeilToInt(damage * (1 - resistance));
            _currentHealth = Mathf.Max(_currentHealth - finalDamage, 0);

            onHealthChangedEvent.Raise(new HealthEventArgs(this, _currentHealth));

            if (IsDead)
            {
                onDeathEvent.Raise(this);
            }
        }

        /// <summary>
        ///  Heals the entity.
        /// </summary>
        /// <param name="amount"> The amount of health to restore. </param>
        public void Heal(int amount)
        {
            if (IsDead) return;

            _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
            onHealthChangedEvent.Raise(new HealthEventArgs(this, _currentHealth));
        }
        
        public bool CanHeal()
        {
            return !IsDead && _currentHealth < maxHealth;
        }

        /// <summary>
        ///  Sets the entity's health to its maximum value.
        /// </summary>
        /// <param name="type"> The type of damage to deal. </param>
        /// <returns></returns>
        private float GetResistance(DamageType type)
        {
            foreach (var resistance in resistances)
            {
                if (resistance.Type == type) return resistance.ResistancePercentage;
            }

            return 0f;
        }
    }
}
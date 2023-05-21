using System.Collections;
using System.Collections.Generic;

namespace SpaceGame.Entities
{
	public interface IDamageable
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }

        event System.Action<int> OnHealthChanged;
        event System.Action<int> OnDie;

        void TakeDamage(int damage);
        void Die();
    }
}

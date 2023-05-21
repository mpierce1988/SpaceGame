using UnityEngine;

namespace SpaceGame.Entities
{
	public abstract class BaseDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth;
         private int _currentHealth;
		public int MaxHealth => _maxHealth;
		public int CurrentHealth => _currentHealth;
		public event System.Action<int> OnHealthChanged;
		public event System.Action<int> OnDie;
		protected virtual void Awake()
        {
			_currentHealth = _maxHealth;
		}
		public virtual void TakeDamage(int damage)
        {
			_currentHealth -= damage;
			
			if (_currentHealth <= 0)
            {
				_currentHealth = 0;
				Die();
			}
			OnHealthChanged?.Invoke(_currentHealth);
		}
		public virtual void Die()
        {
			OnDie?.Invoke(_currentHealth);
		}
	}
}

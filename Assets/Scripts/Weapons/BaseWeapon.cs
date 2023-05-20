using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceGame.Pooling;

namespace SpaceGame.Weapons
{
	public abstract class BaseWeapon : MonoBehaviour, IWeapon
	{
        private Coroutine _coroutine;

        [SerializeField] private float _fireDuration;
		[SerializeField] private float _cooldownDuration;
		[SerializeField] private float _fireRate;
		[SerializeField] private int _damagePerProjectile;
        [SerializeField] private List<Transform> _projectileOrigins;
        [SerializeField] private ObjectPooler _projectilePool;
        

        public float FireDuration => _fireDuration;
        public float CooldownDuration => _cooldownDuration;
        public float FireRate => _fireRate;
        public int DamagePerProjectile => _damagePerProjectile;
        public List<Transform> ProjectileOrigins => _projectileOrigins;
        public ObjectPooler ProjectilePool { get { return _projectilePool; } }

		public event Action OnBeginFiring;
		public event Action OnEndFiring;
		public event Action OnFire;
		public event Action OnBeginCooldown;
		public event Action OnEndCooldown;

        private bool _isFiring = false;

		public void BeginFiring()
        {
            if(_coroutine == null)
            {
                _isFiring = true;
                _coroutine = StartCoroutine(FiringCoroutine());
            }
        }
		public void EndFiring()
        {
            _isFiring = false;

            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private IEnumerator FiringCoroutine()
        {
			float timeSinceStartFiring = 0f;
			bool isCooldown = false;
			// begin firing for as long as my FireDuration
			while (_isFiring)
            {               

                if(!isCooldown)
                {                    
                    // handle firing
                    Fire();
					yield return new WaitForSeconds(1f / FireRate);
                    timeSinceStartFiring += (1f / FireRate);
                    if(timeSinceStartFiring >= _fireDuration)
                    {
                        // switch to cooldown
                        OnEndFiring?.Invoke();
                        OnBeginCooldown?.Invoke();
                        isCooldown = true;
                    }
				}
                else
                {
                    // handle cooldown
                    timeSinceStartFiring = 0;
                    yield return new WaitForSeconds(_cooldownDuration);
                    OnEndCooldown?.Invoke();
                    OnBeginFiring?.Invoke();
                    isCooldown = false;
                } 
            }
        }

        protected virtual void Fire()
        {
            OnFire?.Invoke();
        }
        
	}
}

using SpaceGame.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceGame.Weapons
{
    [RequireComponent(typeof(BaseWeapon))]
    public class WeaponAnimationEvents : MonoBehaviour
    {
        public UnityEvent OnFire;
        public UnityEvent OnBeginFiring;
		public UnityEvent OnEndFiring;
		public UnityEvent OnBeginCooldown;
		public UnityEvent OnEndCooldown;

        private BaseWeapon _baseWeapon;

		private void Awake()
		{
			_baseWeapon = GetComponent<BaseWeapon>();
		}
		// Start is called before the first frame update
		void Start()
        {
            // 
            _baseWeapon.OnFire += HandleOnFire;
            _baseWeapon.OnBeginFiring += HandleBeginFiring;
			_baseWeapon.OnEndFiring += HandleEndFiring;
			_baseWeapon.OnBeginCooldown += HandleBeginCooldown;
			_baseWeapon.OnEndCooldown += HandleEndCooldown;
        }

		private void OnDestroy()
		{
			if(_baseWeapon != null )
			{
				_baseWeapon.OnFire -= HandleOnFire;
				_baseWeapon.OnBeginFiring -= HandleBeginFiring;
				_baseWeapon.OnEndFiring -= HandleEndFiring;
				_baseWeapon.OnBeginCooldown -= HandleBeginCooldown;
				_baseWeapon.OnEndCooldown -= HandleEndCooldown;
			}
		}

		private void HandleEndCooldown()
		{
			OnEndCooldown.Invoke();
		}

		private void HandleBeginCooldown()
		{
			OnBeginCooldown.Invoke();
		}

		private void HandleEndFiring()
		{
			OnEndFiring.Invoke();
		}

		private void HandleBeginFiring()
		{
			OnBeginFiring.Invoke();
		}

		private void HandleOnFire()
		{
			OnFire.Invoke();
		}		
    }
}

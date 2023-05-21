using SpaceGame.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [RequireComponent(typeof(BaseDamageable))]
    public class DamageableObstacleAnimater : MonoBehaviour
    {
        private BaseDamageable _damageable;

		private void Awake()
		{
			_damageable = GetComponent<BaseDamageable>();
		}

		private void Start()
		{
			_damageable.OnDie += _damageable_OnDie;
		}

		private void _damageable_OnDie(int obj)
		{
			// spawn explosion 01 at this location
			GameObject particles = SharedPool.instance.GetItemAndSpawn("explosion_01", this.gameObject.transform.position);
			particles.SetActive(true);
			Debug.Log("Object Died");
		}
	}
}

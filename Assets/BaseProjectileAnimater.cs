using SpaceGame.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [RequireComponent(typeof(BaseProjectile))]
    public class BaseProjectileAnimater : MonoBehaviour
    {
        private BaseProjectile _baseProjectile;

        void Awake()
        {
			_baseProjectile = GetComponent<BaseProjectile>();
		}

        // Start is called before the first frame update
        void Start()
        {
			_baseProjectile.OnProjectileDestroyed += _baseProjectile_OnProjectileDestroyed;
        }

		private void OnDestroy()
		{
			if(_baseProjectile != null)
			{
				_baseProjectile.OnProjectileDestroyed -= _baseProjectile_OnProjectileDestroyed;
			}
		}

		private void _baseProjectile_OnProjectileDestroyed()
		{
			// spawn sparks 01 from SharedPool at this location
            var sparks = SharedPool.instance.GetItemAndSpawn("sparks_01", transform.position, Quaternion.identity);
		}
	}
}

using SpaceGame.Entities;
using SpaceGame.Pooling;
using UnityEngine;

namespace SpaceGame.Weapons
{
	public class ShotgunProjectile : BaseProjectile
    {
		private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
		{

			IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

			if (damageable != null)
			{
				// check if collision gameObject layer is in the targetLayerMask list
				//Debug.Log("Projectile collided with IDamageable named " + collision.gameObject.name);
				if ((TargetLayerMask & (1 << collision.gameObject.layer)) != 0)
				{
					damageable.TakeDamage(Damage);
				}				
			}

			
			if(!this.gameObject.activeInHierarchy)
			{
				// this projectile has already been returned to the pool
				return;
			}

			base.Destroy();
		}
	}
}

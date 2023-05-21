using UnityEngine;

namespace SpaceGame.Entities
{
	public class DamageableObstacle : BaseDamageable
	{
		public override void TakeDamage(int damage)
		{
			base.TakeDamage(damage);
			// Do something specific to this type of damageable
			// Debug.Log(this.name + " took " + damage + " damage");
		}

		public override void Die()
		{
			base.Die();
			Debug.Log(this.name + " died");
			Destroy(this.gameObject);
		}
	}
}

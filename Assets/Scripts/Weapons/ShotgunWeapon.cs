using UnityEngine;

namespace SpaceGame.Weapons
{
	public class ShotgunWeapon : BaseWeapon
    {
		protected override void Fire()
		{
			base.Fire();
            // fire from all projectile origins at once
            foreach(Transform origin in ProjectileOrigins)
            {
                // instantiate a projectile
                GameObject projectile = ProjectilePool.GetObject();

                // make project face same direction as origin point
                projectile.transform.rotation = origin.rotation;

                // set projectile position to origin point
                projectile.transform.position = origin.position;

                // launch projectile in projectile's up direction
                IProjectile projectileInterface = projectile.GetComponent<IProjectile>();
                projectileInterface.Launch(projectile.transform.up);
            }
		}
	}
}

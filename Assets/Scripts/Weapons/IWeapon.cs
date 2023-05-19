using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Weapons
{
	public interface IWeapon
    {
        float FireDuration { get; }
        float CooldownDuration { get; }
        float FireRate { get; }
        int DamagePerProjectile { get; }
        List<Transform> ProjectileOrigins { get; }

        event Action OnBeginFiring;
        event Action OnEndFiring;
        event Action OnFire;
        event Action OnBeginCooldown;
        event Action OnEndCooldown;

        void BeginFiring();
        void EndFiring();
    }
}

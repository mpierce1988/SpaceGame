using System;
using UnityEngine;

namespace SpaceGame.Weapons
{
	public interface IProjectile
    {
        public float Speed { get; }
        public int Damage { get; }
        public LayerMask TargetLayerMask { get; }

        event Action OnProjectileFired;
        event Action OnProjectileDestroyed;
        event Action OnProjectileRemoved;

        void SetupProjectile(float speed, int damage);
        void Launch(Vector2 direction);
        void Destroy();
    }
}

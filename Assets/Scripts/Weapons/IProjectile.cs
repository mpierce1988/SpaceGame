using UnityEngine;

namespace SpaceGame.Weapons
{
	public interface IProjectile
    {
        public float Speed { get; }
        public int Damage { get; }
        public LayerMask TargetLayerMask { get; }

        void SetupProjectile(float speed, int damage);
        void Launch(Vector2 direction);        
    }
}

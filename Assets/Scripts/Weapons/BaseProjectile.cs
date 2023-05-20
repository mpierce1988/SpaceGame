using UnityEngine;
using SpaceGame.Pooling;

namespace SpaceGame.Weapons
{
	public abstract class BaseProjectile : MonoBehaviour, IProjectile, IDestroyedAfterDelay
    {
        private Rigidbody2D _rb;

        [SerializeField] private float _speed = 1f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _timeBeforeDestroyed = 1f;
        private float _timeSinceSpawn = 0f;

        private ReturnToPool _returnToPool;

        public float Speed => _speed;
        public int Damage => _damage;

		public float TimeBeforeDestroyed => _timeBeforeDestroyed;

		public float TimeSinceSpawn => _timeSinceSpawn;

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();

            if(GetComponent<ReturnToPool>() != null)
            {
                _returnToPool = GetComponent<ReturnToPool>();
            }
		}

		private void OnEnable()
		{
            _timeSinceSpawn = 0;
		}

		private void Update()
		{
			_timeSinceSpawn += Time.deltaTime;

            if(_timeSinceSpawn >= _timeBeforeDestroyed)
            {
				Destroy();
			}
		}

		public void SetupProjectile(float speed, int damage)
        {
            _speed = speed;
            _damage = damage;
        }

		public void Launch(Vector2 direction)
        {
            _rb.AddForce(direction * Speed);
        }

		public void Destroy()
		{
            _timeSinceSpawn = 0;
            if(_returnToPool != null)
            {
                _returnToPool.Return();
            }
            else
            {
                Destroy(this.gameObject);
            }
		}
	}
}

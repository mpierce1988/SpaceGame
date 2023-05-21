using SpaceGame.Pooling;
using SpaceGame.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class DestroyParticleAfterDelay : MonoBehaviour, IDestroyedAfterDelay
    {
        [SerializeField] private float _timeBeforeDestroyed = 1f;
        private float _timeSinceSpawn = 0f;
        private ReturnToPool _returnToPool;


		public float TimeBeforeDestroyed => _timeBeforeDestroyed;
		public float TimeSinceSpawn => _timeSinceSpawn;

        void Start()
        {
            _returnToPool = GetComponent<ReturnToPool>();
        }

        public void Destroy()
        {
            if (_returnToPool != null)
            {
                _returnToPool.Return();
            }
            else
            {
                Destroy(this.gameObject);
            }
		}

		void OnEnable()
        {
            _timeSinceSpawn = 0f;
        }

        void Update()
        {
            _timeSinceSpawn += Time.deltaTime;
            if(_timeSinceSpawn >= TimeBeforeDestroyed)
            {
                Destroy();
            }
        }

        void OnDisable()
        {
            _timeSinceSpawn = 0;
        }
    }
}

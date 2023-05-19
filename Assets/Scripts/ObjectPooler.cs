using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace SpaceGame
{
	public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] protected PooledObject ObjectToPool;

        private ObjectPool<GameObject> _pool;

		private void Awake()
		{
			_pool = new ObjectPool<GameObject>(CreateObject(), OnTakeObjectFromPool(), OnReturnObjectToPool());

			List<GameObject> list = new List<GameObject>();
			// instantiate initial items
			for(int i = 0; i < ObjectToPool.DefaultSpawnAmount; i++)
			{
				GameObject itemToSpawn = _pool.Get();
				list.Add(itemToSpawn);
			}

			foreach(GameObject itemToSpawn in list)
			{
				_pool.Release(itemToSpawn);
			}
		}

		protected Action<GameObject> OnReturnObjectToPool()
		{
			return (GameObject objectToReturn) =>
			{
				objectToReturn.transform.parent = this.transform;
				objectToReturn.SetActive(false);
			};
		}

		protected Action<GameObject> OnTakeObjectFromPool()
		{
			return (GameObject objectFromPool) =>
			{
				objectFromPool.transform.parent = null;
				objectFromPool.SetActive(true);
			};
		}

		protected Func<GameObject> CreateObject()
		{
			return () =>
			{
				GameObject objectToSpawn = Instantiate(ObjectToPool.ObjectPrefab);
				objectToSpawn.AddComponent<ReturnToPool>().SetPool(_pool);
				objectToSpawn.transform.parent = this.transform;
				return objectToSpawn;
			};
		}

		public GameObject GetObject()
		{
			return _pool.Get();
		}
    }
}

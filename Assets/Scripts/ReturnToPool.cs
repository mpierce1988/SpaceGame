using UnityEngine;
using UnityEngine.Pool;

namespace SpaceGame
{
	public class ReturnToPool : MonoBehaviour
	{
		[SerializeField] private IObjectPool<GameObject> _pool;

		public void SetPool(IObjectPool<GameObject> pool)
		{
			_pool = pool;
		}

		public void Return()
		{
			_pool.Release(this.gameObject);
		}
	}
}

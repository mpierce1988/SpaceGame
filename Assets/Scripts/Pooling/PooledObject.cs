using UnityEngine;

namespace SpaceGame.Pooling
{
    [System.Serializable]
	public class PooledObject
    {
        public string Key;
        public GameObject ObjectPrefab;
        public int DefaultSpawnAmount;
        public Vector3 DefaultScale = Vector3.one;
    }
}

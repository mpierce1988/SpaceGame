using UnityEngine;

namespace SpaceGame
{
    [System.Serializable]
	public class PooledObject
    {
        public string Key;
        public GameObject ObjectPrefab;
        public int DefaultSpawnAmount;
    }
}

using SpaceGame.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class SharedPool : MonoBehaviour
    {
        [SerializeField] private List<ObjectPooler> _objectPoolers = new List<ObjectPooler>();

        public static SharedPool instance;

        private Dictionary<string, ObjectPooler> _objectPoolerDictionary = new Dictionary<string, ObjectPooler>();
        
        void Awake()
        {
			if(instance == null)
            {
				instance = this;
			}
			else
            {
				Destroy(this.gameObject);
			}

			foreach (var pooler in _objectPoolers)
			{
				_objectPoolerDictionary.Add(pooler.PooledObject.Key, pooler);
			}
		}        
       

        public GameObject GetItem(string key)
        {
            GameObject result = null;

            if(_objectPoolerDictionary.TryGetValue(key, out ObjectPooler objPooler))
            {
                result = objPooler.GetObject();
            }


            return result;
        }

        public GameObject GetItemAndSpawn(string key, Vector2 spawnPoint, Quaternion? rotation = null)
        {
            GameObject result = GetItem(key);
            if(result != null)
            {
                result.transform.position = spawnPoint;
                if (rotation != null)
                    result.transform.rotation = rotation.Value;

                result.SetActive(true);
            }

            return result;
        }
    }
}

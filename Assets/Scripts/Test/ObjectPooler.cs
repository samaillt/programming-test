using System.Collections.Generic;
using UnityEngine;

namespace Eden.Test
{
    public class ObjectPooler : MonoBehaviour
    {
        #region Singleton

        private static ObjectPooler _instance;
        public static ObjectPooler Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion

        [System.Serializable]
        public enum ObjectPoolType
        {
            Particle,
            ParticleBomb
        }

        [System.Serializable]
        public class PoolInfo
        {
            public ObjectPoolType objectPoolType;
            public GameObject objectPrefab;
            public int poolSize;
        }

        public List<PoolInfo> poolInfos;
        public Dictionary<ObjectPoolType, Queue<GameObject>> pools;

        private void Start()
        {
            pools = new Dictionary<ObjectPoolType, Queue<GameObject>>();

            foreach(PoolInfo poolInfo in poolInfos)
            {
                pools[poolInfo.objectPoolType] = new Queue<GameObject>();
                for (int i = 0; i < poolInfo.poolSize; i++)
                {
                    GameObject gameObject = Instantiate(poolInfo.objectPrefab);
                    gameObject.transform.SetParent(transform, false);
                    gameObject.SetActive(false);
                    pools[poolInfo.objectPoolType].Enqueue(gameObject);
                }
            }
        }

        private GameObject TrySpawnObject(ObjectPoolType objectPoolType)
        {
            Queue<GameObject> pool = pools[objectPoolType];
            GameObject gameObject;
            if (pool.Count > 0)
            {
                gameObject = pool.Dequeue();
                return gameObject;
            }
            else
            {
                /* // I could have instanciated a new GO from its prefab but forcing the use of a limited sized pool is a personnal choice
                 
                 gameObject = Instantiate(poolInfos[objectPoolType].objectPrefab);
                 return gameObject;
                 
                */
                throw new FailedToSpawnObjectFromPoolException(objectPoolType, "No more element in the pool, please increase its size");
            }
        }

        private void AddObjectToPool(ObjectPoolType objectPoolType, GameObject gameObject)
        {
            gameObject.transform.SetParent(transform, false);
            pools[objectPoolType].Enqueue(gameObject);
        }

        public bool TrySpawnParticle(out Particle particle)
        {
            try
            {
                GameObject particleGO = TrySpawnObject(ObjectPoolType.Particle);
                particle = particleGO.GetComponent<Particle>();
                particle.Show();
                return true;
            }
            catch (FailedToSpawnObjectFromPoolException exception)
            {
                Debug.LogError(exception.Message);
            }
            particle = null;
            return false;
        }

        public bool TrySpawnParticleBomb(out ParticleBomb particleBomb)
        {
            try
            {
                GameObject particleBombGO = TrySpawnObject(ObjectPoolType.ParticleBomb);
                particleBomb = particleBombGO.GetComponent<ParticleBomb>();
                particleBombGO.SetActive(true);
                return true;
            }
            catch (FailedToSpawnObjectFromPoolException exception)
            {
                Debug.LogError(exception.Message);
            }
            particleBomb = null;
            return false;
        }

        public void AddParticleToPool(Particle particle)
        {
            particle.Hide();
            AddObjectToPool(ObjectPoolType.Particle, particle.gameObject);
            particle.ResetLocalPosition();
        }

        public void AddParticleBombToPool(ParticleBomb particleBomb)
        {
            particleBomb.gameObject.SetActive(false);
            AddObjectToPool(ObjectPoolType.ParticleBomb, particleBomb.gameObject);
            particleBomb.ResetState();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Eden.Test
{
    public class ParticlePool : MonoBehaviour
    {
        #region Singleton

        public static ParticlePool Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        [System.Serializable]
        public class ParticlePoolInfos
        {
            public Particle _particlePrefab;
            public int _poolSize;
        }

        public ParticlePoolInfos _particlePoolInfos;
        private Queue<Particle> _particlePool;

        private void Start()
        {
            _particlePool = new Queue<Particle>();
            for (int i = 0; i < _particlePoolInfos._poolSize; i++)
            {
                Particle particle = Instantiate(_particlePoolInfos._particlePrefab);
                particle.gameObject.SetActive(false);
                AddParticleToPool(particle);
            }
        }

        public Particle SpawnParticle()
        {
            Particle particle;
            if (_particlePool.Count > 0)
            {
                particle = _particlePool.Dequeue();
                particle.Show();
            }
            else
            {
                particle = Instantiate(_particlePoolInfos._particlePrefab);
            }
            return particle;
        }

        public void AddParticleToPool(Particle particle)
        {
            particle.transform.SetParent(transform, false);
            particle.ResetLocalPosition();
            _particlePool.Enqueue(particle);
        }
    }
}
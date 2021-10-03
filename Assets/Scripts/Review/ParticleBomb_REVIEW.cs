/*// I commented the whole script to avoid error from Unity as the mother IParticleEmitter has changed

namespace Eden.Review // Thread #1: The script should be in Eden.Test namespace
{
    using System.Collections.Generic;
    using Eden.Test; // Thread #2: Unnecessary use of Eden.Test namespace as this script sould be in Eden.Test namespace
    using UnityEngine;

    public class ParticleBomb : IParticleEmitter
    {
        [SerializeField] private int _particleCount = 100; // Thread #3: This float field seems not properly named. It may cause missunderstanding of its purpose as it's used to refer the number of particles to spawn when the bomb explodes. Therefore it could be named _particleToSpawn.
        [SerializeField] private Particle _particlePrefab = null; // Thread #4: Particle prefab is not necessarely necessary as we're using a Pooling System. Therefore it could be set in the ObjectPooler data.
        [SerializeField] private float _deceleration = 10.0f;
        [SerializeField] private float _speedMin = 600.0f;
        // Thread #5: Unnecessary line break.
        [SerializeField] private float _speedMax = 900.0f;
        [SerializeField] private Color _startColor = Color.yellow;
        [SerializeField] private Color _endColor = Color.red;

        public List<Particle> _liveParticles = new List<Particle>(); // Thread #6: These fields seems to be common to all classes deriving from IParticleEmitter, therefore it could be handled from IParticleEmitter mother class

        public override int ParticleCount { get { return _liveParticles.Count; } } // Thread #6: These fields seems to be common to all classes deriving from IParticleEmitter, therefore it could be handled from IParticleEmitter mother class

        public void Explode()
        {
            for (int i = 0; i < _particleCount; i++)
            {
                // Thread #7: Variables are not named explicitely in this loop. They may need names corresponding to their roles.
                Vector3 v = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f); // Thread #8: This method spawns particles in a square pattern around the center of the bomb. We may use Vector2.Random.InsideUnitCircle to spawn particles in a circle pattern around the center of the bomb.
                Particle p = Instantiate(_particlePrefab); // Thread #9: Instanciation may be risky if LifeTime of each new GO is not properly known/handled. We may consider a pooling system solving this risk.
                p.Init(2.0f, Color.Lerp(_startColor, _endColor, Random.value), v * Random.Range(_speedMin, _speedMax), _deceleration); // Thread #10: There are a lot params in this Init call. We may consider storing in properly named variable each of this parameter to better understand its behavior at first sight.
                p.transform.SetParent(transform, false);
                _liveParticles.Add(p);
            }
        }

        // Thread #11: As in Thread #6, an Update method handling particles death seems to be common to all classes deriving from IParticleEmitter, therefore it could be handled from IParticleEmitter mother class
        public void Update()
        {
            for (int i = 0; i < _liveParticles.Count; i++)
            {
                if (_liveParticles[i].IsDead())
                {
                    // Thread #12: Destroying may be risky if the particle should be handle next. It also could be not optimized if we instanciate and destroy a lot of particles. We may consider a pooling system solving this risk.
                    Destroy(_liveParticles[i].gameObject);
                    _liveParticles.RemoveAt(i--); // Thread #13: The use of index operation i-- will work for the first particle removal but seems to be an error as it will decrement after the RemoveAt call, and then increment at the new loop turn.
                }
            }
        }
    }
}
// Thread #14: We may need to add #region for Fields & Methods to respect projects nomenclature

*/

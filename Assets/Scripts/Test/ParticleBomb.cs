namespace Eden.Test
{
	using UnityEngine;

	public class ParticleBomb : IParticleEmitter
	{
		#region Fields
		[SerializeField] private int _particleToSpawn = 100;
		[SerializeField] private float _deceleration = 10.0f;
		[SerializeField] private float _minVelocity = 600.0f;
		[SerializeField] private float _maxVelocity = 900.0f;
		[SerializeField] private Color _startColor = Color.yellow;
		[SerializeField] private Color _endColor = Color.red;

		private bool _exploded = false;
        #endregion Fields

        #region Methods
        protected override void Update()
		{
			if (_liveParticles.Count == 0 && _exploded == true)
            {
				ObjectPooler.Instance.AddParticleBombToPool(this);
			}

			base.Update();
		}

		public void Explode()
		{
			for (int i = 0; i < _particleToSpawn; i++)
			{
				Particle newParticle;
				if (ObjectPooler.Instance.TrySpawnParticle(out newParticle))
				{
					Vector3 particleDirection = Random.insideUnitCircle; // Note : only particleDirection.X and particleDirection.Y coordinates will be set with a value [-1; 1]

					float particleLifespan = 2f;
					Color particleInitialColor = Color.Lerp(_startColor, _endColor, Random.value);
					Vector3 particleInitialVelocity = particleDirection * Random.Range(_minVelocity, _maxVelocity);

					newParticle.Init(particleLifespan, particleInitialColor, particleInitialVelocity, _deceleration);
					newParticle.transform.SetParent(transform, false);
					_liveParticles.Add(newParticle);
				}
				else
				{
					Debug.LogError("Could not spawn particle from it's pool");
				}
			}
			_exploded = true;
		}

		public void ResetState()
        {
			_exploded = false;
		}
		#endregion Methods
	}
}
namespace Eden.Test
{
	using UnityEngine;

	public class ParticleBomb : IParticleEmitter
	{
		#region Fields
		[SerializeField] private int _particleCount = 100;
		[SerializeField] private float _deceleration = 10.0f;
		[SerializeField] private float _minVelocity = 600.0f;
		[SerializeField] private float _maxVelocity = 900.0f;
		[SerializeField] private Color _startColor = Color.yellow;
		[SerializeField] private Color _endColor = Color.red;
		#endregion Fields

		#region Methods
		public void Explode()
		{
			for (int i = 0; i < _particleCount; i++)
			{
				Vector3 particleDirection = Random.insideUnitCircle; // Note : only particleVelocity.X and particleVelocity.Y coordinates will be set with a value [-1; 1]
				Particle particle = ParticlePool.Instance.SpawnParticle();
				float particleLifespan = 2f;
				Color particleInitialColor = Color.Lerp(_startColor, _endColor, Random.value);
				Vector3 particleInitialVelocity = particleDirection * Random.Range(_minVelocity, _maxVelocity);

				particle.Init(particleLifespan, particleInitialColor, particleInitialVelocity, _deceleration);
				particle.transform.SetParent(transform, false);
				_liveParticles.Add(particle);
			}
		}
		#endregion Methods
	}
}
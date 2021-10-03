namespace Eden.Test
{
	using UnityEngine;

	public class ParticleEmitter : IParticleEmitter
	{
		#region Fields
		[SerializeField] private float _particleLifeMin = 3.0f;
		[SerializeField] private float _particleLifeMax = 9.0f;
		[SerializeField] private float _particlesBySec = 10;
		[SerializeField] private float _maxAngle = 30.0f;
		[SerializeField] private float _particleMinVelocity = 10.0f;
		[SerializeField] private float _particleMaxVelocity = 10.0f;
		[SerializeField] private float _deceleration = 10.0f;

		private float _particlesToSpawn = 0.0f;
		#endregion Fields

		#region Methods
		protected override void Update()
		{
			_particlesToSpawn += _particlesBySec * Time.deltaTime;

			while (_particlesToSpawn >= 1.0f)
			{
				SpawnParticle();
				_particlesToSpawn -= 1.0f;
			}

			base.Update();
		}

		private void SpawnParticle()
		{
			Particle newParticle;
			if (ObjectPooler.Instance.TrySpawnParticle(out newParticle))
            {
				float life = Random.Range(_particleLifeMin, _particleLifeMax);
				Color color = Random.ColorHSV();

				Quaternion max = Quaternion.Euler(0.0f, 0.0f, _maxAngle);
				Quaternion min = Quaternion.Euler(0.0f, 0.0f, -_maxAngle);

				Vector3 maxVector = max * transform.up;
				Vector3 minVector = min * transform.up;

				Vector3 result = Vector3.Lerp(minVector, maxVector, Random.value);

				float particleSpeed = Random.Range(_particleMinVelocity, _particleMaxVelocity);
				Vector3 velocity = result * particleSpeed;

				newParticle.Init(life, color, velocity, _deceleration, true);
				newParticle.transform.SetParent(transform, false);

				_liveParticles.Add(newParticle);
			}
			else
            {
				Debug.LogError("Could not spawn particle from it's pool");
            }
		}
		#endregion Methods
	}
}
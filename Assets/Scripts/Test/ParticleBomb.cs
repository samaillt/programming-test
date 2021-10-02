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
				Vector3 v = Random.insideUnitCircle; // Note : only v.X and v.Y coordinates will be set with a value [-1; 1]
				Particle p = ParticlePool.Instance.SpawnParticle();
				p.Init(2.0f, Color.Lerp(_startColor, _endColor, Random.value), v * Random.Range(_minVelocity, _maxVelocity), _deceleration);
				p.transform.SetParent(transform, false);
				_liveParticles.Add(p);
			}
		}
		#endregion Methods
	}
}
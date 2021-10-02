namespace Eden.Test
{
	using System.Collections.Generic;
	using UnityEngine;

	public class ParticleBomb : IParticleEmitter
	{
		#region Fields
		[SerializeField] private int _particleCount = 100;
		[SerializeField] private Particle _particlePrefab = null;
		[SerializeField] private float _deceleration = 10.0f;
		[SerializeField] private float _speedMin = 600.0f;

		[SerializeField] private float _speedMax = 900.0f;
		[SerializeField] private Color _startColor = Color.yellow;
		[SerializeField] private Color _endColor = Color.red;

		public List<Particle> _liveParticles = new List<Particle>();
		#endregion Fields

		#region Properties
		public override int ParticleCount { get { return _liveParticles.Count; } }
		#endregion Properties

		#region Methods
		public void Explode()
		{
			for (int i = 0; i < _particleCount; i++)
			{
				Vector3 v = Random.insideUnitCircle; // Note : only v.X and v.Y coordinates will be set with a value [-1; 1]
				Particle p = ParticlePool.Instance.SpawnParticle();
				p.Init(2.0f, Color.Lerp(_startColor, _endColor, Random.value), v * Random.Range(_speedMin, _speedMax), _deceleration);
				p.transform.SetParent(transform, false);
				_liveParticles.Add(p);
			}
		}

		public void Update()
		{
			for (int i = 0; i < _liveParticles.Count; i++)
			{
				if (_liveParticles[i].IsDead())
				{
					Particle deadParticle = _liveParticles[i];
					ParticlePool.Instance.AddParticleToPool(deadParticle);
					_liveParticles.RemoveAt(i);
				}
			}
		}
		#endregion Methods
	}
}
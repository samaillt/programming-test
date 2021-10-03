namespace Eden.Test
{
    using System.Collections.Generic;
    using UnityEngine;

	public abstract class IParticleEmitter : MonoBehaviour
	{
		public int ParticleCount { get { return _liveParticles.Count; } }

		public List<Particle> _liveParticles = new List<Particle>();

		protected virtual void OnEnable()
		{
			try
            {
				TryRegisterEmitter();
			}
			catch (FailedToRegisterEmitterException exception)
			{
				Debug.LogError(exception.Message);
            }
		}

		protected virtual void OnDisable()
		{
			try
			{
				TryUnregisterEmitter();
			}
			catch (FailedToUnregisterEmitterException exception)
			{
				Debug.LogError(exception.Message);
			}
		}

		protected virtual void Update()
        {
			for (int i = 0; i < _liveParticles.Count; i++)
			{
				if (_liveParticles[i].IsDead())
				{
					KillParticleAtIndex(i);
				}
			}
		}

		public void KillHalfParticles()
		{
			int particleCount = _liveParticles.Count;
			for (int i = particleCount - 1; i >= particleCount / 2; i--)
			{
				KillParticleAtIndex(i);
			}
		}

		private void TryRegisterEmitter()
		{
			if (Main.Instance != null)
			{
				Main.Instance.RegisterEmitter(this);
			}
			else
			{
				throw new FailedToRegisterEmitterException();
			}
		}

		private void TryUnregisterEmitter()
		{
			if (Main.Instance != null)
			{
				Main.Instance.UnregisterEmitter(this);
			}
			else
			{
				throw new FailedToUnregisterEmitterException();
			}
		}

		private void KillParticleAtIndex(int index)
		{
			Particle deadParticle = _liveParticles[index];
			ObjectPooler.Instance.AddParticleToPool(deadParticle);
			_liveParticles.RemoveAt(index);
		}
	}
}
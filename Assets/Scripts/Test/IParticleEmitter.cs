namespace Eden.Test
{
	using UnityEngine;

	public abstract class IParticleEmitter : MonoBehaviour
	{
		public abstract int ParticleCount { get; }

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
	}
}
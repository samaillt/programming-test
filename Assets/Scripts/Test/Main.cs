namespace Eden.Test
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using System.Collections;
	using Eden.Tools;

	public class Main : MonoBehaviour
	{
		#region Fields
		[SerializeField] private Text _info = null;
		[SerializeField] private FpsCounter _fpsCounter = null;
		[SerializeField] private float _pannelRefreshInterval = 0.01f;

		private List<IParticleEmitter> _emitters = new List<IParticleEmitter>();
		private static Main _instance = null;
		#endregion Fields

		#region Methods
		public static Main Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<Main>();
				}
				return _instance;
			}
		}

		public static bool HasInstance { get { return _instance != null; } }

		public void RegisterEmitter(IParticleEmitter emitter)
		{
			_emitters.Add(emitter);
		}

		public void UnregisterEmitter(IParticleEmitter emitter)
		{
			_emitters.Remove(emitter);
		}

		private void Start()
		{
			StartCoroutine(UpdateInfo(_pannelRefreshInterval));
		}

        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.K))
			{
				foreach (IParticleEmitter emitter in _emitters)
				{
					emitter.KillHalfParticles();
				}
			}
        }

        private IEnumerator UpdateInfo(float refreshInterval)
		{
			float time = refreshInterval;
			while (true)
			{
				if (time >= refreshInterval)
				{
					int liveParticles = 0;

					foreach (IParticleEmitter emitter in _emitters)
					{
						liveParticles += emitter.ParticleCount;
					}

					_info.text = string.Empty;
					_info.text += "FPS : " + _fpsCounter.Fps;
					_info.text += "\nParticles count : " + liveParticles;
					time -= refreshInterval;
				}

				time += Time.deltaTime;
				yield return null;
			}
		}
		#endregion Methods
	}
}
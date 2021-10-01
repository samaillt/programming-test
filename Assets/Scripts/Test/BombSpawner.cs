namespace Eden.Test
{
	using System.Collections;
	using UnityEngine;

	public class BombSpawner : MonoBehaviour
	{
		[SerializeField] private float _spawnIntervalMin = 1.0f;
		[SerializeField] private float _spawnIntervalMax = 4.0f;
		[SerializeField] private ParticleBomb _bombPrefab = null;
		[SerializeField] private Transform _bombAnchor = null;

		private void Start()
		{
			float delay = Random.Range(_spawnIntervalMin, _spawnIntervalMax);
			StartCoroutine(SpawnNextBomb(delay));
		}

		private IEnumerator SpawnNextBomb(float delay)
		{
			float time = 0.0f;

			while (time < delay)
			{
				yield return null;
				time += Time.deltaTime;
			}

			Vector3 position = new Vector3(
				Random.Range(-50.0f, 50.0f),
				Random.Range(-50.0f, 50.0f),
				0.0f);

			ParticleBomb bomb = Instantiate(_bombPrefab);
			bomb.transform.SetParent(_bombAnchor, false);
			bomb.transform.position = position;
			bomb.Explode();

			float newDelay = Random.Range(_spawnIntervalMin, _spawnIntervalMax);
			StartCoroutine(SpawnNextBomb(newDelay));
		}
	}
}
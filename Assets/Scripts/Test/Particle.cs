namespace Eden.Test
{
	using UnityEngine;
	using UnityEngine.UI;

	public class Particle : MonoBehaviour
	{
		#region Fields
		[SerializeField] private float _colorChangeDurationMin = 0.3f;
		[SerializeField] private float _colorChangeDurationMax = 1.5f;

		private Vector3 _speed = Vector3.zero;
		private float _lifespan = 0.0f;
		private float _colorChangeTime = 0.0f;
		private float _colorChangeDuration = 1.0f;
		private Color _previousColor = Color.white;
		private Color _nextColor = Color.white;
		private float _deceleration = 0.0f;
		private bool _changeColor = false;
		#endregion Fields

		#region Methods
		public void Init(float lifespan, Color color, Vector3 initialSpeed, float deceleration, bool changeColor = false)
		{
			_lifespan = lifespan;
			SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
			sprite.color = color;
			_previousColor = color;
			_colorChangeDuration = Random.Range(_colorChangeDurationMin, _colorChangeDurationMax);
			_speed = initialSpeed;
			_deceleration = deceleration;
			_changeColor = changeColor;
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void ResetLocalPosition()
		{
			transform.localPosition = Vector3.zero;
		}

		private void Update()
		{
			_lifespan -= Time.deltaTime;
			transform.localPosition = transform.localPosition + _speed * Time.deltaTime;
			_speed -= _speed * _deceleration * Time.deltaTime;

			UpdateColor();
		}

		public bool IsDead()
		{
			return _lifespan < 0.0f;
		}

		private void UpdateColor()
		{
			if (_changeColor == false)
			{
				return;
			}

			if (_colorChangeTime > _colorChangeDuration)
			{
				_previousColor = _nextColor;
				_nextColor = Random.ColorHSV();
				_colorChangeTime -= _colorChangeDuration;
			}

			if (GetComponentInChildren<SpriteRenderer>() != null)
			{
				GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(_previousColor, _nextColor, _colorChangeTime / _colorChangeDuration);
			}

			_colorChangeTime += Time.deltaTime;
		}
		#endregion Methods
	}
}
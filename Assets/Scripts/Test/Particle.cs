namespace Eden.Test
{
    using UnityEngine;
	using UnityEngine.UI;

	public class Particle : MonoBehaviour
	{
		#region Fields
		[SerializeField] private float _colorChangeDurationMin = 0.3f;
		[SerializeField] private float _colorChangeDurationMax = 1.5f;
		[SerializeField] private float _fadeOutDuration = 1.0f;
		[SerializeField] private SpriteRenderer _sprite;

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
			_sprite.color = color;
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
			if (_sprite == null)
			{
				return;
			}

			if (_changeColor == true)
            {
				if (_colorChangeTime > _colorChangeDuration)
				{
					_previousColor = _nextColor;
					_nextColor = Random.ColorHSV();
					_colorChangeTime -= _colorChangeDuration;
				}

				TryUpdateAlphaIfFadingOut(ref _nextColor);

				_sprite.color = Color.Lerp(_previousColor, _nextColor, _colorChangeTime / _colorChangeDuration);

				_colorChangeTime += Time.deltaTime;
			}
			else
			{
				Color color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, _sprite.color.a);
				if (TryUpdateAlphaIfFadingOut(ref color))
                {
					_sprite.color = color;
				}
			}
		}

		private bool TryUpdateAlphaIfFadingOut(ref Color color)
        {
			if (_lifespan <= _fadeOutDuration)
			{
				float alpha = color.a;
				color.a = Mathf.Lerp(0, alpha, _lifespan / _fadeOutDuration);
				return true;
			}
			return false;
		}
		#endregion Methods
	}
}
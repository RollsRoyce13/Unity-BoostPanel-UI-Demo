using System;
using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(Image))]
	public class BoosterButton : MonoBehaviour
	{
		public event Action<BoosterSO> OnBoosterButtonClicked;
		
		[Header("References")]
		[SerializeField] private Image highlightImage;
		
		public BoosterSO BoosterSo => _boosterSo;
		
		private Button _button => GetComponent<Button>();
		private Image _buttonImage => _button.image;
		
		private BoosterSO _boosterSo;
		
		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}
		
		private void Start()
		{
			SubscribeToEvents();
		}

		public void Initialize(BoosterSO boosterSo)
		{
			if (boosterSo == null)
			{
				Debug.LogWarning($"BoosterSO is null when initializing button on {gameObject.name}");
				return;
			}
			
			_boosterSo = boosterSo;

			SetSprite(_boosterSo.Sprite);
			DisableHighlight();
		}

		public void DisableHighlight()
		{
			if (highlightImage != null)
			{
				highlightImage.gameObject.SetActive(false);
			}
			else
			{
				Debug.LogWarning($"Highlight image is not assigned on {gameObject.name}");
			}
		}
		
		private void SubscribeToEvents()
		{
			_button.onClick.AddListener(OnButtonClick);
		}

		private void UnsubscribeFromEvents()
		{
			_button.onClick.RemoveListener(OnButtonClick);
		}

		private void OnButtonClick()
		{
			if (highlightImage != null)
			{
				highlightImage.gameObject.SetActive(true);
			}

			OnBoosterButtonClicked?.Invoke(_boosterSo);
		}

		private void SetSprite(Sprite sprite)
		{
			if (_buttonImage != null)
			{
				_buttonImage.sprite = sprite;
			}
			else
			{
				Debug.LogWarning($"Image not assigned on {gameObject.name}");
			}
		}
	}
}
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
		
		[Header("Links")]
		[SerializeField] private Image highlightImage;
		
		public BoosterSO BoosterSo => _boosterSo;
		
		private Button _button => GetComponent<Button>();
		private Image _image => _button.image;
		
		private BoosterSO _boosterSo;
		
		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}
		
		private void Start()
		{
			SubscribeToEvents();
		}

		public void Init(BoosterSO boosterSo)
		{
			_boosterSo = boosterSo;

			SetSprite(_boosterSo.Sprite);
			DisableHighlight();
		}

		public void DisableHighlight()
		{
			highlightImage.gameObject.SetActive(false);
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
			highlightImage.gameObject.SetActive(true);
			
			OnBoosterButtonClicked?.Invoke(_boosterSo);
		}

		private void SetSprite(Sprite sprite)
		{
			if (_image != null)
			{
				_image.sprite = sprite;
			}
			else
			{
				Debug.LogWarning($"Image not assigned on {gameObject.name}");
			}
		}
	}
}
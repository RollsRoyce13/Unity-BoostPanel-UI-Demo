using System;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(Image))]
	public class ItemButton : MonoBehaviour
	{
		public event Action<ItemSO> OnItemButtonClicked;
		
		[Header("Links")]
		[SerializeField] private Image highlightImage;
		[SerializeField] private ParticleSystem particleSystem;
		
		public ItemSO ItemSO => _itemSO;
		
		private Button _button => GetComponent<Button>();
		private Image _image => _button.image;
		
		private ItemSO _itemSO;
		
		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}
		
		private void Start()
		{
			SubscribeToEvents();
		}

		public void Init(ItemSO itemSO)
		{
			_itemSO = itemSO;

			SetSprite(_itemSO.Sprite);
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
			
			OnItemButtonClicked?.Invoke(_itemSO);
		}

		private void PlayParticle()
		{
			if (particleSystem != null)
			{
				particleSystem.Play();
			}
			else
			{
				Debug.LogWarning($"Particle System not assigned on {gameObject.name}");
			}
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
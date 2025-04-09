using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
	[RequireComponent(typeof(Image))]
	public class SlotImage : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField] private bool isLocked = true;
		
		public bool IsLocked => isLocked;
		
		private Image _image;
		private Coroutine _setSpriteCoroutine;

		private void Awake()
		{
			_image = GetComponent<Image>();
		}
		
		public void SetSpriteWithDelay(Sprite sprite, float delay)
		{
			if (_setSpriteCoroutine != null)
			{
				StopCoroutine(_setSpriteCoroutine);
			}
			
			_setSpriteCoroutine = StartCoroutine(SetSpriteDelayed(sprite, delay));
		}

		private IEnumerator SetSpriteDelayed(Sprite sprite, float delay)
		{
			yield return new WaitForSeconds(delay);

			if (_image != null)
			{
				_image.sprite = sprite;
			}
			else
			{
				Debug.LogWarning($"Image not assigned on {gameObject.name}");
			}
			
			_setSpriteCoroutine = null;
		}
	}
}
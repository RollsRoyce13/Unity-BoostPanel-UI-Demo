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
		
		private Image _image => GetComponent<Image>();

		private Coroutine _coroutine;

		public void SetSpriteWithDelay(Sprite sprite, float delay)
		{
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
				_coroutine = null;
			}
			
			_coroutine = StartCoroutine(SetSprite(sprite, delay));
		}

		private IEnumerator SetSprite(Sprite sprite, float delay)
		{
			yield return new WaitForSeconds(delay);

			_image.sprite = sprite;
		}
	}
}
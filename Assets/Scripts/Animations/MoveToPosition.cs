using DG.Tweening;
using UnityEngine;

namespace Boosters
{
	public class MoveToPosition : BaseTween
	{
		public void LocalMove(Vector3 position, float duration)
		{
			_tween = transform.DOLocalMove(position, duration).SetEase(easeType);
		}
		
		public void RectMove(Vector3 position, float duration)
		{
			var rectTransform = GetRectTransform();

			if (rectTransform == null) return;
			
			_tween = rectTransform.DOAnchorPos(position, duration).SetEase(easeType);
		}
		
		public void RectMove(Vector3 position, float duration, Ease ease)
		{
			var rectTransform = GetRectTransform();

			if (rectTransform == null) return;
			
			_tween = rectTransform.DOAnchorPos(position, duration).SetEase(ease);
		}

		private RectTransform GetRectTransform()
		{
			RectTransform rectTransform = GetComponent<RectTransform>();

			if (rectTransform == null)
			{
				Debug.LogWarning($"RectTransform not found on {gameObject.name}.");
				return null;
			}

			return rectTransform;
		}
	}
}
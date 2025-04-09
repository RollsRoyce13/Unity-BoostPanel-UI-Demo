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
			RectTransform rectTransform = GetComponent<RectTransform>();
			
			_tween = rectTransform.DOAnchorPos(position, duration).SetEase(easeType);
		}
		
		public void RectMove(Vector3 position, float duration, Ease ease)
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			
			_tween = rectTransform.DOAnchorPos(position, duration).SetEase(ease);
		}
	}
}
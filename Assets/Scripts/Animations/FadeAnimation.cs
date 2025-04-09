using DG.Tweening;
using UnityEngine;

namespace Boosters
{
	[RequireComponent(typeof(CanvasGroup))]
	public class FadeAnimation : BaseTween
	{
		[SerializeField, Range(0f, 5f)] private float duration = 1f;
		
		private CanvasGroup _canvasGroup => GetComponent<CanvasGroup>();
		
		public void FadeOut()
		{
			_tween = _canvasGroup.DOFade(0f, duration).SetEase(easeType);
		}
		
		public void FadeIn()
		{
			_canvasGroup.DOFade(1f, duration).SetEase(Ease.Linear);
		}
	}
}
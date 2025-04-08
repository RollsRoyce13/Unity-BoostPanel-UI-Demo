using DG.Tweening;
using UnityEngine;

namespace Animations
{
	public abstract class BaseTween : MonoBehaviour
	{
		[Header("Settings")] 
		[SerializeField] protected Ease easeType = Ease.Linear;
		
		protected Tween _tween;

		private void OnDisable()
		{
			StopTween();
		}
		
		private void StopTween()
		{
			if (_tween != null && _tween.IsActive())
			{
				_tween.Kill();
			}
		}
	}
}
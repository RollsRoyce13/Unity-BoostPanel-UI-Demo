using DG.Tweening;
using UnityEngine;

namespace Animations
{
	public class MoveToPosition : BaseTween
	{
		public void Move(Vector3 position, float duration)
		{
			_tween = transform.DOLocalMove(position, duration).SetEase(easeType);
		}
	}
}
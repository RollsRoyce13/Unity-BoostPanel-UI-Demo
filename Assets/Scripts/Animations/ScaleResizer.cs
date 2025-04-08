using DG.Tweening;
using UnityEngine;

namespace Animations
{
	public class ScaleResizer : BaseTween
	{
		public void ChangeSize(Vector3 size, float duration)
		{
			_tween = transform.DOScale(size, duration);
		}
	}
}
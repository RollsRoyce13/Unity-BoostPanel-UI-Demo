using DG.Tweening;
using UnityEngine;

namespace Boosters
{
	public class ScaleResizer : BaseTween
	{
		public void ChangeSize(Vector3 size, float duration)
		{
			_tween = transform.DOScale(size, duration);
		}
	}
}
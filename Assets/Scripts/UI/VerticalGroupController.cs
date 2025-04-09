using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
	[RequireComponent(typeof(MoveToPosition))]
	[RequireComponent(typeof(VerticalLayoutGroup))]
	public class VerticalGroupController : MonoBehaviour
	{
		[Header("Links")]
		[SerializeField] private DeviceAspectRatioSO deviceAspectRatioSo;
		
		[Header("Settings")]
		[SerializeField] private Vector3 closePosition = new Vector3(63f, 0f, 0f);
		[SerializeField, Range(0f, 5f)] private float moveDuration = 1f;
		[SerializeField] private float spacing16To9 = -320f;
		[SerializeField] private float spacingLongScreen = -200f;

		private MoveToPosition _moveToPosition;
		private VerticalLayoutGroup _verticalLayoutGroup;
		
		private Vector3 _startPosition;
		
		private bool _isPanelOpen;

		private void Awake()
		{
			_moveToPosition = GetComponent<MoveToPosition>();
			_verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
			
			Init();
		}
		
		public void SwitchDirection()
		{
			Vector3 direction = _isPanelOpen ? closePosition : _startPosition;
			_isPanelOpen = !_isPanelOpen;
			
			_moveToPosition.RectMove(direction, moveDuration);
		}

		private void Init()
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			
			_isPanelOpen = true;
			_startPosition = rectTransform.anchoredPosition;
			
			if (deviceAspectRatioSo != null)
			{
				float spacing = deviceAspectRatioSo.IsLongScreen()
					? spacingLongScreen
					: spacing16To9;

				_verticalLayoutGroup.spacing = spacing;
			}
			else
			{
				Debug.LogWarning($"Device Aspect Ratio SO not assigned on {gameObject.name}");
			}
		}
	}
}
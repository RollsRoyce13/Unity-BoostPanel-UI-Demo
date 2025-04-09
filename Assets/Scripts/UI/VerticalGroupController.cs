using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(MoveToPosition))]
	[RequireComponent(typeof(VerticalLayoutGroup))]
	public class VerticalGroupController : MonoBehaviour
	{
		[Header("Aspect Ratio Settings")]
		[SerializeField] private DeviceAspectRatioSO deviceAspectRatioSo;
		
		[Header("Panel Positions")]
		[SerializeField] private Vector3 closePosition = new Vector3(63f, 0f, 0f);
		[SerializeField, Range(0f, 5f)] private float moveDuration = 1f;
		
		[Header("Layout Spacing")]
		[SerializeField] private float spacing16To9 = -320f;
		[SerializeField] private float spacingLongScreen = -200f;

		private RectTransform _rectTransform;
		private MoveToPosition _moveToPosition;
		private VerticalLayoutGroup _verticalLayoutGroup;
		
		private Vector3 _startPosition;
		private bool _isPanelOpen;

		private void Awake()
		{
			CacheComponents();
			Initialize();
		}

		public void TogglePanel()
		{
			_isPanelOpen = !_isPanelOpen;
			
			Vector3 direction = _isPanelOpen ? _startPosition : closePosition;
			_moveToPosition.RectMove(direction, moveDuration);
		}

		private void CacheComponents()
		{
			_rectTransform = GetComponent<RectTransform>();
			_moveToPosition = GetComponent<MoveToPosition>();
			_verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
		}

		private void Initialize()
		{
			_isPanelOpen = true;
			_startPosition = _rectTransform.anchoredPosition;
			
			if (deviceAspectRatioSo != null)
			{
				_verticalLayoutGroup.spacing = deviceAspectRatioSo.IsLongScreen()
					? spacingLongScreen
					: spacing16To9;
			}
			else
			{
				Debug.LogWarning($"Device Aspect Ratio SO not assigned on {gameObject.name}");
			}
		}
	}
}
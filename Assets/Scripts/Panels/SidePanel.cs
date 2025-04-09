using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boosters
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(MoveToPosition))]
	public class SidePanel : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private DeviceAspectRatioSO deviceAspectRatioSo;
		[SerializeField] private VerticalGroupController verticalGroupController;
		[SerializeField] private List<SlotImage> slotImagesList;
		
		[Header("Animation Settings")]
		[SerializeField] private Vector3 closePosition = new Vector3(63f, 0f, 0f);
		[SerializeField, Range(0f, 5f)] private float moveDuration = 1f;
		
		private RectTransform _rectTransform;
		private MoveToPosition _moveToPosition;

		private Vector3 _startPosition;
		
		private bool _isPanelOpen;
		private bool _isOpenFrozen;
		private bool _isLongScreen;
		
		private void Awake()
		{
			CacheComponents();
			Initialize();
		}

		public void TogglePanel()
		{
			if (_isOpenFrozen) return;
			
			_isPanelOpen = !_isPanelOpen;
			
			Vector3 direction = _isPanelOpen ? _startPosition : closePosition;
			_moveToPosition.RectMove(direction, moveDuration);

			if (_isLongScreen && verticalGroupController != null)
			{
				verticalGroupController.TogglePanel();
			}
		}

		public void OpenAndFreezePanel()
		{
			if (!_isPanelOpen)
			{
				TogglePanel();
			}
			
			_isOpenFrozen = true;
		}

		public SlotImage GetFreeSlotImage()
		{
			return slotImagesList.FirstOrDefault(slot => slot != null && !slot.IsLocked);
		}

		private void CacheComponents()
		{
			_rectTransform = GetComponent<RectTransform>();
			_moveToPosition = GetComponent<MoveToPosition>();
		}

		private void Initialize()
		{
			_startPosition = _rectTransform.anchoredPosition;
			
			_isPanelOpen = true;
			_isOpenFrozen = false;
			
			if (deviceAspectRatioSo != null)
			{
				_isLongScreen = deviceAspectRatioSo.IsLongScreen();
			}
			else
			{
				Debug.LogWarning($"Device Aspect Ratio SO not assigned on {gameObject.name}");
			}
		}
	}
}
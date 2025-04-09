using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boosters
{
	[RequireComponent(typeof(MoveToPosition))]
	public class SidePanel : MonoBehaviour
	{
		[Header("Links")]
		[SerializeField] private DeviceAspectRatioSO deviceAspectRatioSo;
		[SerializeField] private VerticalGroupController verticalGroupController;
		
		[Header("Lists")]
		[SerializeField] private List<SlotImage> slotImagesList;
		
		[Header("Settings")]
		[SerializeField] private Vector3 closePosition = new Vector3(63f, 0f, 0f);
		[SerializeField, Range(0f, 5f)] private float moveDuration = 1f;
		
		private MoveToPosition _moveToPosition;

		private Vector3 _startPosition;
		
		private bool _isPanelOpen;
		private bool _isOpenFrozen;
		private bool _isLongScreen;
		
		private void Awake()
		{
			_moveToPosition = GetComponent<MoveToPosition>();
			
			Init();
		}

		public void SwitchDirection()
		{
			if (_isOpenFrozen) return;
			
			Vector3 direction = _isPanelOpen ? closePosition : _startPosition;
			_isPanelOpen = !_isPanelOpen;
			
			_moveToPosition.RectMove(direction, moveDuration);

			if (_isLongScreen)
			{
				verticalGroupController.SwitchDirection();
			}
		}
		
		public void OpenAndFreezePanel()
		{
			if (!_isPanelOpen)
			{
				SwitchDirection();
			}
			
			_isOpenFrozen = true;
		}

		public SlotImage GetFreeSlotImage()
		{
			return slotImagesList.FirstOrDefault(x => !x.IsLocked);
		}
		
		private void Init()
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			
			_isPanelOpen = true;
			_isOpenFrozen = false;
			_startPosition = rectTransform.anchoredPosition;
			
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
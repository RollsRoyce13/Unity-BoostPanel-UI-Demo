using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(Animator))]
	public class ButtonClickAnimation : MonoBehaviour
	{
		private static readonly int Pressed = Animator.StringToHash("Pressed");
		
		private Button _button;
		private Animator _animator;

		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}

		private void Awake()
		{
			_button = GetComponent<Button>();
			_animator = GetComponent<Animator>();

			SubscribeToEvents();
		}
		
		private void SubscribeToEvents()
		{
			_button.onClick.AddListener(OnButtonClick);
		}

		private void UnsubscribeFromEvents()
		{
			_button.onClick.RemoveListener(OnButtonClick);
		}

		private void OnButtonClick()
		{
			if (_animator != null)
			{
				_animator.SetTrigger(Pressed);
			}
			else
			{
				Debug.LogWarning($"Animator not assigned on{gameObject.name}");
			}
		}
	}
}
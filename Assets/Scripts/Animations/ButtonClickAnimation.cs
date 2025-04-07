using UnityEngine;
using UnityEngine.UI;

namespace Animations
{
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(Animator))]
	public class ButtonClickAnimation : MonoBehaviour
	{
		private static readonly int Pressed = Animator.StringToHash("Pressed");
		
		private Button _button;
		private Animator _animator;

		private void Awake()
		{
			_button = GetComponent<Button>();
			_animator = GetComponent<Animator>();
			
			_button.onClick.AddListener(StartClickAnimation);
		}

		private void StartClickAnimation()
		{
			_animator.SetTrigger(Pressed);
		}
	}
}
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(MoveToPosition))]
	[RequireComponent(typeof(ScaleResizer))]
	public class BoosterImage : MonoBehaviour
	{
		[Header("Links")]
		[SerializeField] private Image checkmarkImage;
		[SerializeField] private new ParticleSystem particleSystem;
		
		[Header("Animation Settings")]
		[SerializeField] private Vector3 centerSize = new Vector3(1.2f, 1.2f, 1.0f);
		[SerializeField] private Vector3 slotsSize = new Vector3(0.55f, 0.55f, 1.0f);
		[SerializeField] private Ease slotsEase;
		[SerializeField, Range(0f, 5f)] private float animationDuration = 1f;
		
		private Image _image => GetComponent<Image>();
		private MoveToPosition _moveToPosition => GetComponent<MoveToPosition>();
		private ScaleResizer _scaleResizer =>  GetComponent<ScaleResizer>();

		private SidePanel _sidePanel;
		private BoosterSO _booster;
		
		private Coroutine _coroutine;
		
		private void Start()
		{
			StartAnimate();
		}

		public void Init(SidePanel sidePanel, BoosterSO booster)
		{
			_sidePanel = sidePanel;
			_booster = booster;
			
			_image.sprite = _booster.Sprite;
		}

		private void StartAnimate()
		{
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
				_coroutine = null;
			}

			_coroutine = StartCoroutine(ChangeStates());
		}

		private IEnumerator ChangeStates()
		{
			_moveToPosition.LocalMove(Vector3.zero, animationDuration);
			_scaleResizer.ChangeSize(centerSize, animationDuration);

			yield return new WaitForSeconds(animationDuration);
			
			_sidePanel.OpenAndFreezePanel();
			ActivateCheckmark();
			PlayParticle();
			
			yield return new WaitForSeconds(animationDuration);

			MoveToSlot();
			Destroy(gameObject, animationDuration);
		}

		private void MoveToSlot()
		{
			var slotImage = _sidePanel.GetFreeSlotImage();

			if (slotImage != null)
			{
				slotImage.SetSpriteWithDelay(_booster.Sprite, animationDuration);
				transform.SetParent(slotImage.transform);
			
				_moveToPosition.RectMove(Vector3.zero, animationDuration, slotsEase);
				_scaleResizer.ChangeSize(slotsSize, animationDuration);
			}
			else
			{
				Debug.LogWarning("slotImage is null");
			}
		}

		private void ActivateCheckmark()
		{
			if (checkmarkImage != null)
			{
				checkmarkImage.gameObject.SetActive(true);
			}
			else
			{
				Debug.LogWarning($"Checkmark image not assigned on {gameObject.name}");
			}
		}

		private void PlayParticle()
		{
			if (particleSystem != null)
			{
				particleSystem.Play();
			}
			else
			{
				Debug.LogWarning($"Particle System not assigned on {gameObject.name}");
			}
		}
	}
}
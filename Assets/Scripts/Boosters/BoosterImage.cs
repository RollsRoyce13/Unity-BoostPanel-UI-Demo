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
		[Header("References")]
		[SerializeField] private Image checkmarkImage;
		[SerializeField] private new ParticleSystem particleSystem;
		
		[Header("Animation Settings")]
		[SerializeField] private Vector3 centerSize = new Vector3(1.2f, 1.2f, 1.0f);
		[SerializeField] private Vector3 slotsSize = new Vector3(0.55f, 0.55f, 1.0f);
		[SerializeField] private Ease slotsEase;
		[SerializeField, Range(0f, 5f)] private float animationDuration = 1f;
		
		private Image _image;
		private MoveToPosition _moveToPosition;
		private ScaleResizer _scaleResizer;

		private SidePanel _sidePanel;
		private BoosterSO _booster;
		
		private Coroutine _animationCoroutine;
		
		private void Awake()
		{
			CacheComponents();
			StartAnimationSequence();
		}
		
		public void Initialize(SidePanel sidePanel, BoosterSO booster)
		{
			if (sidePanel == null || booster == null)
			{
				Debug.LogError("Initialization failed: SidePanel or BoosterSO is null.");
				return;
			}
            
			_sidePanel = sidePanel;
			_booster = booster;
			
			UpdateImage();
		}

		private void CacheComponents()
		{
			_image = GetComponent<Image>();
			_moveToPosition = GetComponent<MoveToPosition>();
			_scaleResizer = GetComponent<ScaleResizer>();
		}

		private void StartAnimationSequence()
		{
			if (_animationCoroutine != null)
			{
				StopCoroutine(_animationCoroutine);
			}

			_animationCoroutine = StartCoroutine(AnimateSequence());
		}

		private void UpdateImage()
		{
			_image.sprite = _booster.Sprite;
		}

		private IEnumerator AnimateSequence()
		{
			PlayEntryAnimation();

			yield return new WaitForSeconds(animationDuration);
			
			SetSelectedState();

			yield return new WaitForSeconds(animationDuration);

			MoveToSlot();
			Destroy(gameObject, animationDuration);
			
			_animationCoroutine = null;
		}

		private void PlayEntryAnimation()
		{
			_moveToPosition.LocalMove(Vector3.zero, animationDuration);
			_scaleResizer.ChangeSize(centerSize, animationDuration);
		}

		private void SetSelectedState()
		{
			_sidePanel.OpenAndFreezePanel();
			ActivateCheckmark();
			PlayParticle();
		}

		private void MoveToSlot()
		{
			var slotImage = _sidePanel.GetFreeSlotImage();

			if (slotImage == null)
			{
				Debug.LogWarning("No free slot image found.");
				return;
			}
			
			slotImage.SetSpriteWithDelay(_booster.Sprite, animationDuration);
			transform.SetParent(slotImage.transform);
			
			_moveToPosition.RectMove(Vector3.zero, animationDuration, slotsEase);
			_scaleResizer.ChangeSize(slotsSize, animationDuration);
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
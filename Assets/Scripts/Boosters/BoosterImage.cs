using System.Collections;
using Animations;
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
		
		[Header("Settings")]
		[SerializeField] private Vector3 centerSize = new Vector3(1.2f, 1.2f, 1.0f);
		[SerializeField, Min(0f)] private float moveToCenterDuration = 1f;
		
		private Image _image => GetComponent<Image>();
		private MoveToPosition _moveToPosition => GetComponent<MoveToPosition>();
		private ScaleResizer ScaleResizer =>  GetComponent<ScaleResizer>();

		private void Start()
		{
			StartCoroutine(Animate());
		}

		public void SetSprite(Sprite sprite)
		{
			_image.sprite = sprite;
		}

		private IEnumerator Animate()
		{
			_moveToPosition.Move(Vector3.zero, moveToCenterDuration);
			ScaleResizer.ChangeSize(centerSize, moveToCenterDuration);

			yield return new WaitForSeconds(moveToCenterDuration);
			
			ActivateCheckmark();
			PlayParticle();
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
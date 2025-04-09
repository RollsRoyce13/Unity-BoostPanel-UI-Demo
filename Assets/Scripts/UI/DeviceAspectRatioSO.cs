using UnityEngine;

namespace Boosters
{
	[CreateAssetMenu (menuName = "Device Aspect Ratio/Device Aspect Ratio")]
	public class DeviceAspectRatioSO : ScriptableObject
	{
		[Tooltip("This is threshold value for Iphone 8, Iphone SE 2 etc.")]
		[SerializeField] private float aspectRatioThreshold16To9 = 1.8f;
		[SerializeField] private bool isVerticalScreen = true;

		public bool IsLongScreen()
		{
			float aspectRatio = 0;

			if (isVerticalScreen)
			{
				aspectRatio = (float)Screen.height / Screen.width;
			}
			else
			{
				aspectRatio = (float)Screen.width / Screen.height;
			}
			
			Debug.Log($"Screen Width: {Screen.width}, Height: {Screen.height}. " +
			          $"Current aspect ratio is: {aspectRatio}");
			
			return aspectRatio > aspectRatioThreshold16To9;
		}
	}
}
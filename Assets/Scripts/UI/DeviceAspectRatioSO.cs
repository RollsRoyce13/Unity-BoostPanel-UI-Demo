using UnityEngine;

namespace Boosters
{
	[CreateAssetMenu (menuName = "Device Aspect Ratio/Device Aspect Ratio")]
	public class DeviceAspectRatioSO : ScriptableObject
	{
		[Header("Settings")]
		[Tooltip("Threshold value for 16:9 screens (e.g., iPhone 8, iPhone SE 2, etc.).")]
		[SerializeField] private float aspectRatioThreshold16To9 = 1.8f;
		[SerializeField] private bool isVerticalScreen = true;

		public bool IsLongScreen()
		{
			float aspectRatio = CalculateAspectRatio();

#if UNITY_EDITOR
			Debug.Log($"Screen Width: {Screen.width}, Height: {Screen.height}. " +
			          $"Aspect Ratio: {aspectRatio}, Threshold: {aspectRatioThreshold16To9}");
#endif

			return aspectRatio > aspectRatioThreshold16To9;
		}
		
		private float CalculateAspectRatio()
		{
			return isVerticalScreen
				? (float)Screen.height / Screen.width
				: (float)Screen.width / Screen.height;
		}
	}
}
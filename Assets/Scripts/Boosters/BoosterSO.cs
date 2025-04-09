using UnityEngine;

namespace Boosters
{
	[CreateAssetMenu (menuName = "Boosters/Booster")]
	public class BoosterSO : ScriptableObject
	{
		[SerializeField] private BoosterType boosterType;
		[SerializeField] private Sprite sprite;
		
		public BoosterType BoosterType => boosterType;
		public Sprite Sprite => sprite;
	}
}
using Enums;
using UnityEngine;

namespace Boosters
{
	[CreateAssetMenu (menuName = "Boosters/Booster")]
	public class BoosterSO : ScriptableObject
	{
		[field: SerializeField] public BoosterType BoosterType { get; private set; }
		[field: SerializeField] public Sprite Sprite { get; private set; }
	}
}
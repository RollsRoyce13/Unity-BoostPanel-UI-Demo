using Enums;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu (menuName = "Items/Item")]
	public class ItemSO : ScriptableObject
	{
		[field: SerializeField] public BoosterType BoosterType { get; private set; }
		[field: SerializeField] public Sprite Sprite { get; private set; }
	}
}
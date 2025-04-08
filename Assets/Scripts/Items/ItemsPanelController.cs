using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Items
{
	public class ItemsPanelController : MonoBehaviour
	{
		[Header("Events")]
		public UnityEvent OnRefreshClicked;
		public UnityEvent<ItemSO> OnItemClicked;
		public UnityEvent<ItemSO> OnItemSelected;
		
		[Header("Items List")]
		[SerializeField] private List<ItemSO> itemsList;
		[SerializeField] private List<ItemButton> itemButtonsList;

		private const int TWO_ITEMS = 2;
		private const int THREE_ITEMS = 3;
		
		private List<ItemSO> _currentItemsList = new List<ItemSO>();

		private ItemSO _currentSelectedItem;

		private void OnDisable()
		{
			UnsubscribeFromEvents();
		}

		private void OnEnable()
		{
			Refresh();
			SubscribeToEvents();
		}

		public void Refresh()
		{
			List<ItemSO> shuffledItems = ShuffleItemsList();
			List<ItemSO> newItems = GetTwoUniqueItems(shuffledItems);
			
			// Check newItems count and fill with missing items
			while (newItems.Count < TWO_ITEMS)
			{
				var filler = shuffledItems.FirstOrDefault(item => !newItems.Contains(item) && !_currentItemsList.Contains(item));
				
				if (filler != null)
				{
					newItems.Add(filler);
				}
				else
				{
					break;
				}
			}
			
			if (_currentItemsList.Count == 0)
			{
				_currentItemsList = shuffledItems.Take(THREE_ITEMS).ToList();
			}
			else
			{
				FillCurrentItemsList(newItems, shuffledItems);
			}
			
			InitButtons();
			OnRefreshClicked?.Invoke();
		}

		public void Ok()
		{
			OnItemSelected?.Invoke(_currentSelectedItem);
			
			Debug.Log($"Booster: {_currentSelectedItem.BoosterType} was selected");
		}

		private void SubscribeToEvents()
		{
			foreach (var itemButton in itemButtonsList)
			{
				itemButton.OnItemButtonClicked += ClickItem;
			}
		}

		private void UnsubscribeFromEvents()
		{
			foreach (var itemButton in itemButtonsList)
			{
				itemButton.OnItemButtonClicked -= ClickItem;
			}
		}

		private void FillCurrentItemsList(List<ItemSO> newItems, List<ItemSO> shuffledItems)
		{
			ItemSO repeatedItem = GetRepeatedItemFromList();
				
			_currentItemsList = newItems.Take(TWO_ITEMS).ToList();
        
			if (repeatedItem != null && !_currentItemsList.Contains(repeatedItem))
			{
				_currentItemsList.Add(repeatedItem);
			}
				
			if (_currentItemsList.Count < THREE_ITEMS)
			{
				var additionalItem = shuffledItems.FirstOrDefault(item => !_currentItemsList.Contains(item));
					
				if (additionalItem != null)
				{
					_currentItemsList.Add(additionalItem);
				}
			}
		}

		private void ClickItem(ItemSO item)
		{
			_currentSelectedItem = item;
			OnItemClicked?.Invoke(_currentSelectedItem);
			
			DisableHighlight();
			Debug.Log($"Button with booster: {_currentSelectedItem.BoosterType} was clicked");
		}

		private void InitButtons()
		{
			for (var i = 0; i < _currentItemsList.Count; i++)
			{
				itemButtonsList[i].Init(_currentItemsList[i]);
			}
		}
		
		private void DisableHighlight()
		{
			foreach (var itemButton in itemButtonsList)
			{
				if (itemButton.ItemSO == _currentSelectedItem) continue;
				
				itemButton.DisableHighlight();
			}
		}
		
		private List<ItemSO> ShuffleItemsList()
		{
			List<ItemSO> shuffledItems = itemsList
				.OrderBy(_ => Random.value)
				.ToList();

			return shuffledItems;
		}
		
		private List<ItemSO> GetTwoUniqueItems(List<ItemSO> items)
		{
			List<ItemSO> newItems = items
				.Where(item => !_currentItemsList.Contains(item))
				.Take(TWO_ITEMS)
				.ToList();

			return newItems;
		}

		private ItemSO GetRepeatedItemFromList()
		{
			ItemSO repeatedItem = null;
			
			repeatedItem = _currentItemsList
				.OrderBy(_ => Random.value)
				.FirstOrDefault();

			return repeatedItem;
		}
	}
}
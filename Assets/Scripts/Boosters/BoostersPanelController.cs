using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Boosters
{
	public class BoostersPanelController : MonoBehaviour
	{
		[Header("Events")]
		public UnityEvent OnRefreshClicked;
		public UnityEvent<BoosterSO> OnItemClicked;
		public UnityEvent<BoosterSO> OnItemSelected;
		
		[Header("Items List")]
		[SerializeField] private List<BoosterSO> itemsList;
		[SerializeField] private List<BoosterButton> itemButtonsList;
		
		[Header("Item Image Prefab")] 
		[SerializeField] private BoosterImage boosterImagePrefab;

		private const int TWO_ITEMS = 2;
		private const int THREE_ITEMS = 3;
		
		private List<BoosterSO> _currentItemsList = new List<BoosterSO>();

		private BoosterSO _currentSelectedBooster;

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
			List<BoosterSO> shuffledItems = ShuffleItemsList();
			List<BoosterSO> newItems = GetTwoUniqueItems(shuffledItems);
			
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
			SpawnItemImage();
			OnItemSelected?.Invoke(_currentSelectedBooster);
			
			Debug.Log($"Booster: {_currentSelectedBooster.BoosterType} was selected");
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

		private void FillCurrentItemsList(List<BoosterSO> newItems, List<BoosterSO> shuffledItems)
		{
			BoosterSO repeatedBooster = GetRepeatedItemFromList();
				
			_currentItemsList = newItems.Take(TWO_ITEMS).ToList();
        
			if (repeatedBooster != null && !_currentItemsList.Contains(repeatedBooster))
			{
				_currentItemsList.Add(repeatedBooster);
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

		private void ClickItem(BoosterSO booster)
		{
			_currentSelectedBooster = booster;
			OnItemClicked?.Invoke(_currentSelectedBooster);
			
			DisableHighlight();
			Debug.Log($"Button with booster: {_currentSelectedBooster.BoosterType} was clicked");
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
				if (itemButton.BoosterSo == _currentSelectedBooster) continue;
				
				itemButton.DisableHighlight();
			}
		}

		private void SpawnItemImage()
		{
			BoosterButton targetObject = itemButtonsList
				.FirstOrDefault(x => x.BoosterSo == _currentSelectedBooster);

			if (targetObject != null)
			{
				RectTransform targetRectTransform = targetObject.GetComponent<RectTransform>();
				
				BoosterImage boosterImage = Instantiate(boosterImagePrefab, transform, false);

				boosterImage.SetSprite(_currentSelectedBooster.Sprite);
				boosterImage.transform.localPosition = targetRectTransform.localPosition;
			}
		}
		
		private List<BoosterSO> ShuffleItemsList()
		{
			List<BoosterSO> shuffledItems = itemsList
				.OrderBy(_ => Random.value)
				.ToList();

			return shuffledItems;
		}
		
		private List<BoosterSO> GetTwoUniqueItems(List<BoosterSO> items)
		{
			List<BoosterSO> newItems = items
				.Where(item => !_currentItemsList.Contains(item))
				.Take(TWO_ITEMS)
				.ToList();

			return newItems;
		}

		private BoosterSO GetRepeatedItemFromList()
		{
			BoosterSO repeatedBooster = null;
			
			repeatedBooster = _currentItemsList
				.OrderBy(_ => Random.value)
				.FirstOrDefault();

			return repeatedBooster;
		}
	}
}
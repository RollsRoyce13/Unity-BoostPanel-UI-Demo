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
		public UnityEvent<BoosterSO> OnBoosterClicked;
		public UnityEvent<BoosterSO> OnBoosterSelected;
		
		[Header("Boosters List")]
		[SerializeField] private List<BoosterSO> boostersList;
		[SerializeField] private List<BoosterButton> boosterButtonsList;
		
		[Header("Booster Image Prefab")] 
		[SerializeField] private BoosterImage boosterImagePrefab;

		private const int TWO_BOOSTERS = 2;
		private const int THREE_BOOSTERS = 3;
		
		private List<BoosterSO> _currentBoostersList = new List<BoosterSO>();

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
			List<BoosterSO> shuffledBoosters = ShuffleBoostersList();
			List<BoosterSO> newBoosters = GetTwoUniqueBoosters(shuffledBoosters);
			
			// Check newBoosters count and fill with missing boosters
			while (newBoosters.Count < TWO_BOOSTERS)
			{
				var filler = shuffledBoosters.FirstOrDefault(booster => !newBoosters.Contains(booster) && !_currentBoostersList.Contains(booster));
				
				if (filler != null)
				{
					newBoosters.Add(filler);
				}
				else
				{
					break;
				}
			}
			
			if (_currentBoostersList.Count == 0)
			{
				_currentBoostersList = shuffledBoosters.Take(THREE_BOOSTERS).ToList();
			}
			else
			{
				FillCurrentBoostersList(newBoosters, shuffledBoosters);
			}
			
			InitButtons();
			OnRefreshClicked?.Invoke();
		}

		public void Ok()
		{
			SpawnBoosterImage();
			OnBoosterSelected?.Invoke(_currentSelectedBooster);
			
			Debug.Log($"Booster: {_currentSelectedBooster.BoosterType} was selected");
		}

		private void SubscribeToEvents()
		{
			foreach (var boosterButton in boosterButtonsList)
			{
				boosterButton.OnBoosterButtonClicked += ClickBooster;
			}
		}

		private void UnsubscribeFromEvents()
		{
			foreach (var boosterButton in boosterButtonsList)
			{
				boosterButton.OnBoosterButtonClicked -= ClickBooster;
			}
		}

		private void FillCurrentBoostersList(List<BoosterSO> newBoosters, List<BoosterSO> shuffledBoosters)
		{
			BoosterSO repeatedBooster = GetRepeatedBoosterFromList();
			
			_currentBoostersList = newBoosters.Take(TWO_BOOSTERS).ToList();
        
			if (repeatedBooster != null && !_currentBoostersList.Contains(repeatedBooster))
			{
				_currentBoostersList.Add(repeatedBooster);
			}
				
			if (_currentBoostersList.Count < THREE_BOOSTERS)
			{
				var additionalBooster = shuffledBoosters.FirstOrDefault(booster => !_currentBoostersList.Contains(booster));
					
				if (additionalBooster != null)
				{
					_currentBoostersList.Add(additionalBooster);
				}
			}
		}

		private void ClickBooster(BoosterSO booster)
		{
			_currentSelectedBooster = booster;
			OnBoosterClicked?.Invoke(_currentSelectedBooster);
			
			DisableHighlight();
			Debug.Log($"Button with booster: {_currentSelectedBooster.BoosterType} was clicked");
		}

		private void InitButtons()
		{
			for (var i = 0; i < _currentBoostersList.Count; i++)
			{
				boosterButtonsList[i].Init(_currentBoostersList[i]);
			}
		}
		
		private void DisableHighlight()
		{
			foreach (var boosterButton in boosterButtonsList)
			{
				if (boosterButton.BoosterSo == _currentSelectedBooster) continue;
				
				boosterButton.DisableHighlight();
			}
		}

		private void SpawnBoosterImage()
		{
			BoosterButton targetObject = boosterButtonsList
				.FirstOrDefault(x => x.BoosterSo == _currentSelectedBooster);

			if (targetObject != null)
			{
				RectTransform targetRectTransform = targetObject.GetComponent<RectTransform>();
				
				BoosterImage boosterImage = Instantiate(boosterImagePrefab, transform, false);

				boosterImage.SetSprite(_currentSelectedBooster.Sprite);
				boosterImage.transform.localPosition = targetRectTransform.localPosition;
			}
		}
		
		private List<BoosterSO> ShuffleBoostersList()
		{
			List<BoosterSO> shuffledBoosters = boostersList
				.OrderBy(_ => Random.value)
				.ToList();

			return shuffledBoosters;
		}
		
		private List<BoosterSO> GetTwoUniqueBoosters(List<BoosterSO> boosters)
		{
			List<BoosterSO> newBoosters = boosters
				.Where(booster => !_currentBoostersList.Contains(booster))
				.Take(TWO_BOOSTERS)
				.ToList();

			return newBoosters;
		}

		private BoosterSO GetRepeatedBoosterFromList()
		{
			BoosterSO repeatedBooster = null;
			
			repeatedBooster = _currentBoostersList
				.OrderBy(_ => Random.value)
				.FirstOrDefault();

			return repeatedBooster;
		}
	}
}
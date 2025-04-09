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

		[Header("References")]
		[SerializeField] private SidePanel sidePanel;
		
		[Header("Boosters Data")]
		[SerializeField] private List<BoosterSO> boostersList;
		[SerializeField] private List<BoosterButton> boosterButtonsList;
		
		[Header("Booster UI Prefab")] 
		[SerializeField] private BoosterImage boosterImagePrefab;

		private const int BOOSTERS_COUNT_MIN = 2;
		private const int BOOSTERS_COUNT_MAX = 3;
		
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
			var shuffledBoosters = ShuffleBoostersList();
			var newBoosters = GetTwoUniqueBoosters(shuffledBoosters);
			
			// Check newBoosters count and fill with missing boosters
			while (newBoosters.Count < BOOSTERS_COUNT_MIN)
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
				_currentBoostersList = shuffledBoosters.Take(BOOSTERS_COUNT_MAX).ToList();
			}
			else
			{
				UpdateCurrentBoosterList(newBoosters, shuffledBoosters);
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
				boosterButtonsList[i].Initialize(_currentBoostersList[i]);
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
			var boosterButton = boosterButtonsList.FirstOrDefault(x => x.BoosterSo == _currentSelectedBooster);

			if (boosterButton == null) return;
			
			BoosterImage boosterImage = Instantiate(boosterImagePrefab, transform, false);

			boosterImage.Initialize(sidePanel, _currentSelectedBooster);
			boosterImage.transform.localPosition = boosterButton.GetComponent<RectTransform>().localPosition;
		}

		private List<BoosterSO> ShuffleBoostersList()
		{
			return boostersList.OrderBy(_ => Random.value).ToList();
		}

		private List<BoosterSO> GetTwoUniqueBoosters(List<BoosterSO> boosters)
		{
			return boosters.Where(b => !_currentBoostersList.Contains(b))
				.Take(BOOSTERS_COUNT_MIN).ToList();
		}

		private void UpdateCurrentBoosterList(List<BoosterSO> newBoosters, List<BoosterSO> shuffledBoosters)
		{
			var repeatedBooster = GetRepeatedBooster();
			
			_currentBoostersList = newBoosters.Take(BOOSTERS_COUNT_MIN).ToList();
        
			if (repeatedBooster != null && !_currentBoostersList.Contains(repeatedBooster))
			{
				_currentBoostersList.Add(repeatedBooster);
			}
				
			if (_currentBoostersList.Count < BOOSTERS_COUNT_MAX)
			{
				var additionalBooster = shuffledBoosters.FirstOrDefault(booster => !_currentBoostersList.Contains(booster));
					
				if (additionalBooster != null)
				{
					_currentBoostersList.Add(additionalBooster);
				}
			}
		}

		private BoosterSO GetRepeatedBooster()
		{
			return _currentBoostersList.OrderBy(_ => Random.value).FirstOrDefault();
		}
	}
}
using Infrastructure;
using UnityEngine;
using Zenject;
using TMPro;

public class GameUIWindow : Window
{
	[Inject] private IStorageService _storageService; 
	
	[SerializeField] private TMP_Text _energyText;
	[SerializeField] private TMP_Text _minesText;
	[SerializeField] private GameObject _gameEndScreen;
	[SerializeField] private GameObject _levelWinScreen;
	[SerializeField] private GameObject _pauseScreen;

	public override void Show(UIViewArguments arguments)
	{
		base.Show(arguments);

		_storageService.OnEnergyChanged += UpdateEnergyText;
		_storageService.OnMinesChanged += UpdateMinesText;

		Reset();
	}
	
	public void ShowLevelComplete()
	{
		_levelWinScreen.SetActive(true);
	}
	
	public void CloseLevelComplete()
	{
		_levelWinScreen.SetActive(false);
	}
	
	public void ShowEndGame()
	{
		_gameEndScreen.SetActive(true);
	}
	
	public void CloseEndGame()
	{
		_gameEndScreen.SetActive(false);
	}
	
	public void ShowPauseMenu()
	{
		_pauseScreen.SetActive(true);
	}
	
	public void ClosePauseMenu()
	{
		_pauseScreen.SetActive(false);
	}

	public override void Hide(UIViewArguments arguments)
	{
		base.Hide(arguments);
		
		_storageService.OnEnergyChanged -= UpdateEnergyText;
		_storageService.OnMinesChanged -= UpdateMinesText;
	}

	public override void Reset()
	{
		CloseLevelComplete();
		ClosePauseMenu();
		CloseEndGame();
		UpdateView();
	}

	private void UpdateEnergyText(int value) => _energyText.text = value.ToString();
	private void UpdateMinesText(int value) => _minesText.text = value.ToString();

	private void UpdateView()
	{
		UpdateEnergyText(_storageService.Energy);
		UpdateMinesText(_storageService.Mines);
	}
}
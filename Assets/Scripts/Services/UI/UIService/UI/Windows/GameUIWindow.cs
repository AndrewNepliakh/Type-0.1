using UnityEngine.SceneManagement;
using Infrastructure;
using UnityEngine;
using Signals;
using Zenject;
using TMPro;

public class GameUIWindow : Window
{
	[Inject] private SignalBus _signalBus;
	[Inject] private IStorageService _storageService; 
	
	[SerializeField] private GameObject _levelWinScreen;
	[SerializeField] private GameObject _gameEndScreen;
	[SerializeField] private GameObject _pauseScreen;
	[SerializeField] private TMP_Text   _energyText;
	[SerializeField] private TMP_Text   _minesText;

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
		Time.timeScale = 0;
		_signalBus.Fire<GamePaused>();
		_pauseScreen.SetActive(true);
	}
	
	public void ClosePauseMenu()
	{
		Time.timeScale = 1;
		_pauseScreen.SetActive(false);
	}
	
	public void OpenMenu()
	{
		SceneManager.LoadScene(Constants.MENU_SCENE);
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
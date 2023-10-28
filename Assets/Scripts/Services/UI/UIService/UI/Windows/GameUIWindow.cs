using Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

public class GameUIWindow : Window
{
	[Inject] private IStorageService _storageService; 
	
	[SerializeField] private TMP_Text _energyText;
	[SerializeField] private TMP_Text _minesText;
	[SerializeField] private GameObject _gameEndScreen;

	public override void Show(UIViewArguments arguments)
	{
		base.Show(arguments);

		_storageService.OnEnergyChanged += UpdateEnergyText;
		_storageService.OnMinesChanged += UpdateMinesText;

		Reset();
	}
	
	public void ShowEndGame()
	{
		_gameEndScreen.SetActive(true);
	}
	
	public void CloseEndGame()
	{
		_gameEndScreen.SetActive(false);
	}

	public override void Hide(UIViewArguments arguments)
	{
		base.Hide(arguments);
		
		_storageService.OnEnergyChanged -= UpdateEnergyText;
		_storageService.OnMinesChanged -= UpdateMinesText;
	}

	public override void Reset()
	{
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
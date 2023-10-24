using Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

public class GameUIWindow : Window
{
	[Inject] private IStorageService _storageService; 
	
	[SerializeField] private TMP_Text _energyText;
	[SerializeField] private TMP_Text _minesText;

	public override void Show(UIViewArguments arguments)
	{
		base.Show(arguments);

		_storageService.OnEnergyChanged += UpdateEnergyText;
		_storageService.OnMinesChanged += UpdateMinesText;

		UpdateView();
	}

	public override void Hide(UIViewArguments arguments)
	{
		base.Hide(arguments);
		
		_storageService.OnEnergyChanged -= UpdateEnergyText;
		_storageService.OnMinesChanged -= UpdateMinesText;
	}

	public override void Reset()
	{
	}

	private void UpdateEnergyText(int value) => _energyText.text = value.ToString();
	private void UpdateMinesText(int value) => _minesText.text = value.ToString();

	private void UpdateView()
	{
		UpdateEnergyText(_storageService.Energy);
		UpdateMinesText(_storageService.Mines);
	}

}
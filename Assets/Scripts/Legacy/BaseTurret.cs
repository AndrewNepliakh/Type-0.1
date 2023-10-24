using Infrastructure;
using Services.Factory;
using Services.UI;
using UnityEngine;
using Zenject;

public class BaseTurret : MonoBehaviour
{
	[Inject] private IStorageService _storageService;
	[Inject] private IGameObjectsFactory _gameObjectsFactory;

	[SerializeField] private PowerIsRequired300 _powerIsRequiredPrefab;
	[SerializeField] private GameObject _buildCanvas;
	[SerializeField] private GameObject _aura;
	[SerializeField] private Color _hoverColorWhite;
	[SerializeField] private Color _hoverColorDark;
	private Color _startColorWhite;
	private Color _startColorDark;
	private Material[] _rend;
	private GameObject _energyRequireBool;

	private void Start()
	{
		_rend = GetComponent<Renderer>().materials;
		_buildCanvas.SetActive(false);
		_startColorWhite = _rend[0].color;
		_startColorDark = _rend[1].color;
	}
	
	private void OnMouseEnter()
	{
		_buildCanvas.SetActive(true);

		_rend[0].color = _hoverColorWhite;
		_rend[1].color = _hoverColorDark;
	}

	private void OnMouseExit()
	{
		_buildCanvas.SetActive(false);

		_rend[0].color = _startColorWhite;
		_rend[1].color = _startColorDark;
	}

	private void OnMouseDown()
	{
		if (_storageService.Energy < 300 && _energyRequireBool == null)
		{
			_energyRequireBool = _gameObjectsFactory.InstantiateSingleGameObject<PowerIsRequired300>(_powerIsRequiredPrefab.gameObject, transform.position, transform.rotation);
		}
		else if (_storageService.Energy >= 300)
		{
			GameObject turretToBuild = BuildManager.instanse.GetTurretToBuild();
			Instantiate(turretToBuild, transform.position, transform.rotation);
			_storageService.SubtractEnergy(300);
			Destroy(_aura);
		}
	}
}
using UnityEngine.Serialization;
using Services.InputService;
using Services.Factory;
using Infrastructure;
using Services.UI;
using UnityEngine;
using Services;
using Zenject;
using Data;

public class TurretUpgrade : MonoBehaviour
{
	[Inject] private IGameObjectsFactory _gameObjectsFactory;
	[Inject] private IStorageService _storageService;
	[Inject] private IBuildService _buildService;
	[Inject] private IInputService _inputService;

	[SerializeField] private Color _hoverColorWhite;
	[SerializeField] private Color _hoverColorDark;

	[FormerlySerializedAs("matsLafet")] [SerializeField]
	private Material[] _matsLafet;

	[FormerlySerializedAs("matsAmmo")] [SerializeField]
	private Material[] _matsAmmo;

	[FormerlySerializedAs("matsGun")] [SerializeField]
	private Material[] _matsGun;

	[FormerlySerializedAs("lafet")] [SerializeField]
	private GameObject _lafet;

	[FormerlySerializedAs("ammo")] [SerializeField]
	private GameObject _ammo;

	[FormerlySerializedAs("gun")] [SerializeField]
	private GameObject _gun;

	[FormerlySerializedAs("upgradeCanvas")] [SerializeField]
	private GameObject _upgradeCanvas;

	[FormerlySerializedAs("objectToUpgrade")] [SerializeField]
	private GameObject _objectToUpgrade;

	[FormerlySerializedAs("powerIsRequiredUi")] [SerializeField]
	private GameObject _powerIsRequiredPrefab;

	private Color _startColorWhite;
	private Color _startColorDark;
	private GameObject _energyRequireAlert;


	private void Start()
	{
		_upgradeCanvas.SetActive(false);

		_matsLafet = _lafet.GetComponent<Renderer>().materials;
		_matsAmmo = _ammo.GetComponent<Renderer>().materials;
		_matsGun = _gun.GetComponent<Renderer>().materials;

		_startColorDark = _matsLafet[0].color;
		_startColorWhite = _matsLafet[1].color;
	}

	private void OnMouseEnter()
	{
		if(_inputService.IsOverUI()) return;
		
		_upgradeCanvas.SetActive(true);
		_matsLafet[0].color = _hoverColorDark;
		_matsAmmo[0].color = _hoverColorDark;
		_matsGun[0].color = _hoverColorDark;

		_matsLafet[1].color = _hoverColorWhite;
		_matsAmmo[1].color = _hoverColorWhite;
		_matsGun[1].color = _hoverColorWhite;
	}

	private void OnMouseExit()
	{
		_upgradeCanvas.SetActive(false);
		_matsLafet[0].color = _startColorDark;
		_matsAmmo[0].color = _startColorDark;
		_matsGun[0].color = _startColorDark;

		_matsLafet[1].color = _startColorWhite;
		_matsAmmo[1].color = _startColorWhite;
		_matsGun[1].color = _startColorWhite;
	}

	private void OnMouseDown()
	{
		if(_inputService.IsOverUI()) return;
		
		var cost = _buildService.GetBuildItemCost(BuildType.PlasmaTurret);
		
		if (_storageService.Energy < cost && _energyRequireAlert == null)
		{
			_energyRequireAlert =
				_gameObjectsFactory.InstantiateSingleGameObject<PowerIsRequired600>(_powerIsRequiredPrefab.gameObject,
					transform.position, transform.rotation);
		}
		else if (_storageService.Energy >= cost)
		{
			var turretToBuild = _buildService.GetItemToBuild(BuildType.PlasmaTurret);
			Instantiate(turretToBuild, transform.position, transform.rotation);
			_storageService.SubtractEnergy(cost);
			Destroy(gameObject);
		}
	}
}
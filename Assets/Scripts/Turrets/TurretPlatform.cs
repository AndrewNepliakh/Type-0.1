﻿using Services.InputService;
using Services.Factory;
using Infrastructure;
using UnityEngine;
using Services.UI;
using Services;
using Zenject;
using Data;

public class TurretPlatform : MonoBehaviour
{
	[Inject] private IGameObjectsFactory _gameObjectsFactory;
	[Inject] private IStorageService _storageService;
	[Inject] private IBuildService _buildService;
	[Inject] private IInputService _inputService;

	[SerializeField] private PowerIsRequired300 _powerIsRequiredPrefab;
	[SerializeField] private GameObject _buildCanvas;
	[SerializeField] private Color _hoverColorWhite;
	[SerializeField] private Color _hoverColorDark;
	[SerializeField] private GameObject _aura;
	private Color _startColorWhite;
	private Color _startColorDark;
	private Material[] _rend;
	private GameObject _energyRequireAlert;

	private void Start()
	{
		_rend = GetComponent<Renderer>().materials;
		_buildCanvas.SetActive(false);
		_startColorWhite = _rend[0].color;
		_startColorDark = _rend[1].color;
	}

	private void OnMouseEnter()
	{
		if(_inputService.IsOverUI()) return;
		
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
		if(_inputService.IsOverUI()) return;
		
		var cost = _buildService.GetBuildItemCost(BuildType.Turret);

		if (_storageService.Energy < cost && _energyRequireAlert == null)
		{
			_energyRequireAlert =
				_gameObjectsFactory.InstantiateSingleGameObject<PowerIsRequired300>(_powerIsRequiredPrefab.gameObject,
					transform.position, transform.rotation);
		}
		else if (_storageService.Energy >= cost)
		{
			var turretToBuild = _buildService.GetItemToBuild(BuildType.Turret);
			_gameObjectsFactory.InstantiateNonSingleGameObject(turretToBuild, transform.position, transform.rotation);
			_storageService.SubtractEnergy(cost);
			Destroy(_aura);
		}
	}
}
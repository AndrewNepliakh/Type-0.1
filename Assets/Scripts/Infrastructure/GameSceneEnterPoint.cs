using Services.Factory;
using UnityEngine;
using Signals;
using Zenject;
using Player;

public class GameSceneEnterPoint : MonoBehaviour
{
	[Inject] private SignalBus _signalBus;
	[Inject] private IUIService _uiService;
	[Inject] private IGameObjectsFactory _gameObjectsFactory;

	[SerializeField] private GameObject _enemyResp;
	[SerializeField] private Vector3 _enemyRespPosition;

	[SerializeField] private GameObject _ownResp;
	[SerializeField] private Vector3 _ownRespPosition;

	[SerializeField] private GameObject _player;
	[SerializeField] private Vector3 _playerPosition;

	[SerializeField] private GameObject _terminal;
	[SerializeField] private Vector3 _terminalPosition;

	[SerializeField] private GameObject _ultimateGun;
	[SerializeField] private Vector3 _ultimateGunPosition;

	[SerializeField] private GameObject _startBattery;
	[SerializeField] private Vector3[] _startBatteryPositions;

	[SerializeField] private GameObject _turretPlatform;
	[SerializeField] private Vector3[] _turretPlatformPosition;

	private void Awake()
	{
		_signalBus.Subscribe<GameEndSignal>(GameEnd);
		_signalBus.Subscribe<LevelCompleteSignal>(LevelCompleteWin);
		_signalBus.Subscribe<GameRestartSignal>(GameRestart);

		InitializeScene();
	}

	private void Start()
	{
		InitializeUI();
	}

	private void InitializeUI()
	{
		_uiService.ShowWindow<GameUIWindow>();
	}

	private void InitializeScene()
	{
		_gameObjectsFactory
			.InstantiateSingleGameObject<PlayerController>(_player, _playerPosition, _player.transform.rotation);
		_gameObjectsFactory.InstantiateSingleGameObject<OwnResp>(_ownResp, _ownRespPosition,
			_ownResp.transform.rotation);
		_gameObjectsFactory.InstantiateSingleGameObject<EnemyResp>(_enemyResp, _enemyRespPosition,
			_enemyResp.transform.rotation);
		_gameObjectsFactory.InstantiateSingleGameObject<TerminalController>(_terminal, _terminalPosition,
			_terminal.transform.rotation);
		_gameObjectsFactory.InstantiateSingleGameObject<UltimateGun>(_ultimateGun, _ultimateGunPosition,
			_ultimateGun.transform.rotation);

		foreach (var position in _startBatteryPositions)
		{
			_gameObjectsFactory.InstantiateNonSingleGameObject(_startBattery, position,
				_startBattery.transform.rotation);
		}
		
		foreach (var position in _turretPlatformPosition)
		{
			_gameObjectsFactory.InstantiateNonSingleGameObject(_turretPlatform, position,
				_turretPlatform.transform.rotation);
		}
	}
	
	private void LevelCompleteWin()
	{
		_uiService.ShowWindow<GameUIWindow>().ShowLevelComplete();
	}

	private void GameEnd()
	{
		_uiService.ShowWindow<GameUIWindow>().ShowEndGame();
	}

	private void GameRestart()
	{
		_gameObjectsFactory.DestroyAllNonSingleObjects();
		_gameObjectsFactory.DestroySingleGameObject<PlayerController>();
		_gameObjectsFactory.DestroySingleGameObject<UltimateGun>();
		_signalBus.Fire<GameLateRestartSignal>();
	}

	private void OnDestroy()
	{
		_signalBus.Unsubscribe<GameEndSignal>(GameEnd);
		_signalBus.Unsubscribe<LevelCompleteSignal>(LevelCompleteWin);
		_signalBus.Unsubscribe<GameRestartSignal>(GameRestart);
	}
}
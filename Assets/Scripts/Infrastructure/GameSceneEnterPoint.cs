using Player;
using Services.Factory;
using Signals;
using UnityEngine;
using Zenject;

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
   
   private void Awake()
   {
      _signalBus.Subscribe<GameEndSignal>(GameEnd);
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
      _gameObjectsFactory.InstantiateSingleGameObject<OwnResp>(_ownResp, _ownRespPosition,_ownResp.transform.rotation);
      _gameObjectsFactory.InstantiateSingleGameObject<PlayerController>(_player, _playerPosition,_player.transform.rotation);
      _gameObjectsFactory.InstantiateSingleGameObject<EnemyResp>(_enemyResp, _enemyRespPosition, _enemyResp.transform.rotation);
      _gameObjectsFactory.InstantiateSingleGameObject<TerminalController>(_terminal, _terminalPosition, _terminal.transform.rotation);
      _gameObjectsFactory.InstantiateSingleGameObject<UltimateGun>(_ultimateGun, _ultimateGunPosition, _ultimateGun.transform.rotation);
   }
   
   private void GameEnd()
   {
      _uiService.ShowWindow<GameUIWindow>().ShowEndGame();
   }
}

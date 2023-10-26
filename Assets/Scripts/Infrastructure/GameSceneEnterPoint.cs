using Player;
using Services.Factory;
using UnityEngine;
using Zenject;

public class GameSceneEnterPoint : MonoBehaviour
{
   [Inject] private IUIService _uiService;
   [Inject] private IGameObjectsFactory _gameObjectsFactory;
   
   [SerializeField] private GameObject _enemyResp;
   [SerializeField] private Vector3 _enemyRespPosition; 
   
   [SerializeField] private GameObject _ownResp;
   [SerializeField] private Vector3 _ownRespPosition;
   
   [SerializeField] private GameObject _player;
   [SerializeField] private Vector3 __playerPosition;
   
   [SerializeField] private GameObject _terminal;
   [SerializeField] private Vector3 __terminalPosition;
   
   [SerializeField] private GameObject _ultimateGun;
   [SerializeField] private Vector3 __ultimateGunPosition;
   
   private void Awake()
   {
      _gameObjectsFactory.InstantiateSingleGameObject<OwnResp>(_ownResp, _ownRespPosition,_ownResp.transform.rotation);
      _gameObjectsFactory.InstantiateSingleGameObject<PlayerController>(_player, __playerPosition,_player.transform.rotation);
      _gameObjectsFactory.InstantiateSingleGameObject<EnemyResp>(_enemyResp, _enemyRespPosition, _enemyResp.transform.rotation);
      _gameObjectsFactory.InstantiateSingleGameObject<TerminalController>(_terminal, __terminalPosition, _terminal.transform.rotation);
      _gameObjectsFactory.InstantiateSingleGameObject<UltimateGun>(_ultimateGun, __ultimateGunPosition, _ultimateGun.transform.rotation);
   }

   private void Start()
   {
      InitializeUI();
   }

   private void InitializeUI()
   {
      _uiService.ShowWindow<GameUIWindow>();
   }
}

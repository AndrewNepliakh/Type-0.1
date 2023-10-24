using UnityEngine;
using Zenject;

public class GameSceneEnterPoint : MonoBehaviour
{
   [Inject] private IUIService _uiService;
   
   private void Awake()
   {
      
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

using Data;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [SerializeField] private BuildData _buildData;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_buildData);
    }
}
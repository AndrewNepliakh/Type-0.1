using Data;
using Services.Spawn;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [SerializeField] private BuildData _buildData;
    [SerializeField] private SpawnData _spawnData;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_buildData);
        Container.BindInstance(_spawnData);
    }
}
using Infrastructure;
using Services;
using Services.Factory;
using Services.Spawn;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IBuildService>().To<BuildService>().AsSingle().NonLazy();
        Container.Bind<ISpawnService>().To<SpawnService>().AsSingle().NonLazy();
        Container.Bind<IGameObjectsFactory>().To<GameObjectsFactory>().AsSingle().NonLazy();
        
        Container.Bind(typeof(IUIService),typeof(IInitializable)).To<UIService>().AsSingle().NonLazy();
        Container.Bind(typeof(IStorageService),typeof(IInitializable)).To<StorageService>().AsSingle().NonLazy();
    }
}
using Infrastructure;
using Services.Factory;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IStorageService>().To<StorageService>().AsSingle().NonLazy();
        Container.Bind<IGameObjectsFactory>().To<GameObjectsFactory>().AsSingle().NonLazy();
        Container.Bind(typeof(IUIService),typeof(IInitializable)).To<UIService>().AsSingle().NonLazy();
    }
}
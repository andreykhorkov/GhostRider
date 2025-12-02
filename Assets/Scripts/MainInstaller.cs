using Models;
using Zenject;

namespace DefaultNamespace
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GhostRiderFacade>().AsSingle().NonLazy();
            Container.BindInterfacesTo<StravaAuthenticator>().AsSingle();
            Container.BindInterfacesTo<FakeStravaActivityDataLoader>().AsSingle();
            Container.BindInterfacesTo<HttpManager>().AsSingle();
            Container.BindInterfacesTo<StravaDataProvider>().AsSingle();
        }
    }
}
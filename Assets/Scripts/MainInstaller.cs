using Models;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        [SerializeField] private bool m_IsFake;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GhostRiderFacade>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HttpManager>().AsSingle();

            if (m_IsFake)
            {
                Container.BindInterfacesTo<FakeAuthenticator>().AsSingle();
                Container.BindInterfacesTo<FakeStravaDataProvider>().AsSingle();
            }
            else
            {
                Container.BindInterfacesTo<StravaActivityDataLoader>().AsSingle();
                Container.BindInterfacesTo<StravaAuthenticator>().AsSingle();
                Container.BindInterfacesTo<StravaDataProvider>().AsSingle();
            }
        }
    }
}
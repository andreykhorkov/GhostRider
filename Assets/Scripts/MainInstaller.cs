using DefaultNamespace.CoordinatesConverter;
using DefaultNamespace.Track;
using Models;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        [SerializeField] private bool m_IsFake;
        [SerializeField] private Material m_WaypointMaterial;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GhostRiderFacade>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HttpManager>().AsSingle();
            Container.BindInterfacesTo<TrackRenderer>().AsSingle();
            Container.BindInterfacesTo<EnuConverter>().AsSingle();
            Container.BindInstance(m_WaypointMaterial);

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
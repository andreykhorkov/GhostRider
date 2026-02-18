using DefaultNamespace.CoordinatesConverter;
using DefaultNamespace.Track;
using Models;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        [System.Serializable]
        public class Installables
        {
            public Material m_WaypointMaterial;
            public LineRenderer m_LineRenderer;
            public Object m_FollowerPrefab;
            public Transform m_NorthDirectionTf;
            public Transform m_SelfTf;
        }

        [SerializeField] private bool m_IsFake;
        [SerializeField] private Installables m_Installables;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GhostRiderFacade>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HttpManager>().AsSingle();
            Container.Bind<ITrackCreator>().FromSubContainerResolve().
                ByInstaller<StravaTrackCreatorInstaller>().AsSingle();
            Container.BindInterfacesTo<TrackRenderer>().AsSingle();
            Container.BindInterfacesTo<EnuConverter>().AsSingle();
            Container.BindInterfacesTo<SelfTrackFollower>().AsSingle();
            Container.BindInstance(m_Installables);
            Container.BindMemoryPool<ITrackFollower, GhostTrackFollower.Pool>().FromSubContainerResolve().
                ByNewContextPrefab(m_Installables.m_FollowerPrefab);

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
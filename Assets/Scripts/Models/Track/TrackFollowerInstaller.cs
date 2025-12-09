using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class TrackFollowerInstaller : MonoInstaller<TrackFollowerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TrackFollower>().AsSingle();
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
        }
    }
}
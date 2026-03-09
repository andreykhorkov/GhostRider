using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class GhostTrackFollowerInstaller : MonoInstaller<GhostTrackFollowerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GhostTrackFollower>().AsSingle();
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
        }
    }
}
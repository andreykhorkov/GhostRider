using Zenject;

namespace DefaultNamespace.Track
{
    public class SelfTrackFollowerInstaller : Installer<SelfTrackFollowerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SelfTrackFollower>().AsSingle();
            Container.BindInterfacesTo<TrackRenderer>().AsSingle();
        }
    }
}
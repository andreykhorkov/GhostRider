using Zenject;

namespace DefaultNamespace.Track
{
    public class DebugTrackFollowerInstaller : Installer<DebugTrackFollowerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DebugSelfTrackFollower>().AsSingle();
            Container.BindInterfacesTo<TrackRenderer>().AsSingle();
        }
    }
}
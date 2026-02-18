using Zenject;

namespace DefaultNamespace.Track
{
    public class StravaTrackCreatorInstaller : Installer<StravaTrackCreatorInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FakeCompass>().AsSingle();
            Container.BindInterfacesTo<StravaTrackCreator>().AsSingle();
        }
    }
}
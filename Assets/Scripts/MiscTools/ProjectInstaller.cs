using MiscTools;
using Zenject;

namespace DefaultNamespace
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<HttpManager>().AsSingle();
            Container.Bind<Dispatcher>().AsSingle();
        }
    }
}
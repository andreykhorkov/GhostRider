using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI
{
    public class ActivityMenuRowInstaller : Installer<ActivityMenuRowInstaller>
    {
        private readonly ActivityAttributes m_RowAttributes;

        public ActivityMenuRowInstaller(ActivityAttributes rowAttributes)
        {
            m_RowAttributes = rowAttributes;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(m_RowAttributes).AsSingle();
            Container.Bind<ActivityMenuRow>().AsSingle();
            Container.Bind<ActivityMenuRowView>().FromComponentOnRoot().AsSingle();
            Container.Bind<Button>().FromComponentOnRoot().AsSingle();
        }
    }
}
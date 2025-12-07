using System;
using Zenject;

namespace DefaultNamespace.UI
{
    public struct ActivityAttributes
    {
        public long Id { get; }
        public DateTime Date { get; }
        public string Name { get; }
        public double Distance { get; }
        public int Elapsed { get; }
        public string Location { get; }

        public ActivityAttributes(long id, DateTime date, string name, double distance, int elapsed, string location)
        {
            Id = id;
            Date = date;
            Name = name;
            Distance = distance;
            Elapsed = elapsed;
            Location = location;
        }
    }

    public class ActivitiesMenuInstaller : Installer<ActivitiesMenuInstaller>
    {
        private readonly ActivitiesMenuInstallables m_ActivityMenuInstallables;

        public ActivitiesMenuInstaller(ActivitiesMenuInstallables activityMenuInstallables)
        {
            m_ActivityMenuInstallables = activityMenuInstallables;
        }

        public override void InstallBindings()
        {
            Container.Bind<ActivitiesMenu>().AsSingle();
            Container.BindInstance(m_ActivityMenuInstallables).AsSingle();
            Container.BindFactory<ActivityAttributes, ActivityMenuRow, ActivityMenuRow.Factory>()
                .FromSubContainerResolve().ByNewPrefabInstaller<ActivityMenuRowInstaller>
                    (m_ActivityMenuInstallables.activitiesRowPrefab).UnderTransform(m_ActivityMenuInstallables.activityRowParent);
        }
    }
}
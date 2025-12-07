using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

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
        private readonly Object m_ActivityRowPrefab;
        private readonly Transform m_ActivityRowParent;

        public ActivitiesMenuInstaller(Object activityRowPrefab, Transform activityRowParent)
        {
            m_ActivityRowPrefab = activityRowPrefab;
            m_ActivityRowParent = activityRowParent;
        }

        public override void InstallBindings()
        {
            Container.Bind<ActivitiesMenu>().AsSingle();
            Container.BindFactory<ActivityAttributes, ActivityMenuRow, ActivityMenuRow.Factory>()
                .FromSubContainerResolve().ByNewPrefabInstaller<ActivityMenuRowInstaller>(m_ActivityRowPrefab)
                .UnderTransform(m_ActivityRowParent);
        }
    }
}
using DefaultNamespace.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller<UIInstaller>
{
    [SerializeField] private Object m_ActivitiesRowPrefab;
    [SerializeField] private Transform m_ActivityRowParent;

    public override void InstallBindings()
    {
        Container.BindInstance(m_ActivitiesRowPrefab);
        Container.BindInstance(m_ActivityRowParent);
        Container.Bind<ActivitiesMenu>().FromSubContainerResolve().ByInstaller<ActivitiesMenuInstaller>().AsSingle().NonLazy();
    }
}

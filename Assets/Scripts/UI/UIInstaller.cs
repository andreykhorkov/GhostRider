using System;
using DefaultNamespace.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

[Serializable]
public class ActivitiesMenuInstallables
{
    public Object activitiesRowPrefab;
    public Transform activityRowParent;
    public GameObject activityMenuObject;
    public Button activityMenuBtn;
}

public class UIInstaller : MonoInstaller<UIInstaller>
{
    [SerializeField] private ActivitiesMenuInstallables m_ActivityMenuInstallables;
    [SerializeField] private Button m_StateButton;

    public override void InstallBindings()
    {
        Container.BindInstance(m_ActivityMenuInstallables);
        Container.BindInstance(m_StateButton);
        Container.Bind<UIFacade>().AsSingle().NonLazy();
        Container.Bind<ActivitiesMenu>().FromSubContainerResolve().ByInstaller<ActivitiesMenuInstaller>().AsSingle()
            .NonLazy();
    }
}
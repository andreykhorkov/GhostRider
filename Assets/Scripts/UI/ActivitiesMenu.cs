using DefaultNamespace.EventArgs;
using MiscTools;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class ActivitiesMenu
    {
        private readonly ActivityMenuRow.Factory m_ActivityRowFactory;
        private readonly Button m_MenuBtn;
        private readonly GameObject m_MenuObj;
        private readonly Dispatcher m_Dispatcher;

        public ActivitiesMenu(Dispatcher dispatcher, ActivityMenuRow.Factory activityRowFactory,
            ActivitiesMenuInstallables installables)
        {
            m_ActivityRowFactory = activityRowFactory;
            m_MenuBtn = installables.activityMenuBtn;
            m_MenuObj = installables.activityMenuObject;
            m_Dispatcher = dispatcher;

            m_Dispatcher.Subscribe(EventId.ActivityIdsRetrieved, OnActivityIdsRetrieved);
            m_Dispatcher.Subscribe(EventId.ActivityTrackCreated, OnActivityTrackCreated);
            m_MenuBtn.onClick.AddListener(OnMenuBtnClick);
        }

        ~ActivitiesMenu()
        {
            m_Dispatcher.Unsubscribe(EventId.ActivityIdsRetrieved, OnActivityIdsRetrieved);
            m_Dispatcher.Unsubscribe(EventId.ActivityTrackCreated, OnActivityTrackCreated);
            m_MenuBtn.onClick.RemoveListener(OnMenuBtnClick);
        }

        private void OnActivityTrackCreated(System.EventArgs _)
        {
            m_MenuObj.SetActive(false);
        }

        private void OnMenuBtnClick()
        {
            m_MenuObj.SetActive(!m_MenuObj.activeInHierarchy);
        }

        private void OnActivityIdsRetrieved(System.EventArgs args)
        {
            var eventArgs = (ActivitiesAttributesEventArgs)args;

            foreach (var attribute in eventArgs.ActivitiesAttributes)
            {
                m_ActivityRowFactory.Create(attribute);
            }
        }
    }
}
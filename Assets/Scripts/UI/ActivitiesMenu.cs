using DefaultNamespace.EventArgs;
using MiscTools;

namespace DefaultNamespace.UI
{
    public class ActivitiesMenu
    {
        private readonly ActivityMenuRow.Factory m_ActivityRowFactory;
        private readonly Dispatcher m_Dispatcher;

        public ActivitiesMenu(Dispatcher dispatcher, ActivityMenuRow.Factory activityRowFactory)
        {
            m_ActivityRowFactory = activityRowFactory;
            m_Dispatcher = dispatcher;
            m_Dispatcher.Subscribe(EventId.ActivityIdsRetrieved, OnActivityIdsRetrieved);
        }

        ~ActivitiesMenu()
        {
            m_Dispatcher.Unsubscribe(EventId.ActivityIdsRetrieved, OnActivityIdsRetrieved);
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
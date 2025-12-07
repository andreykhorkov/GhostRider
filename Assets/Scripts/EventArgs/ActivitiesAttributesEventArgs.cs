using DefaultNamespace.UI;

namespace DefaultNamespace.EventArgs
{
    public class ActivitiesAttributesEventArgs : System.EventArgs
    {
        public ActivityAttributes[] ActivitiesAttributes { get; }

        public ActivitiesAttributesEventArgs(ActivityAttributes[] activitiesAttributes)
        {
            ActivitiesAttributes = activitiesAttributes;
        }
    }
}
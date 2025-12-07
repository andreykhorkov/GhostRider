namespace DefaultNamespace.EventArgs
{
    public class LoadActivityEventArgs : System.EventArgs
    {
        public long ActivityId { get; }

        public LoadActivityEventArgs(long activityId)
        {
            ActivityId = activityId;
        }
    }
}
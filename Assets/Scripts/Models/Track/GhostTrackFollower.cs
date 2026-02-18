using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class GhostTrackFollower : ITrackFollower, ITickable
    {
        private float m_CurrentTime;
        private int m_CurrentWaypointIndex;

        private readonly Transform m_Transform;

        public TrackData TrackData { get; private set; }

        public GhostTrackFollower(Transform transform)
        {
            m_Transform = transform;
        }

        void ITickable.Tick()
        {
            if (TrackData.Waypoints == null || TrackData.Waypoints.Length < 2)
            {
                return;
            }

            if (TrackData.Time == null || TrackData.Time.Length != TrackData.Waypoints.Length)
            {
                return;
            }

            m_CurrentTime += Time.deltaTime;

            while (m_CurrentWaypointIndex < TrackData.Waypoints.Length - 1 &&
                   m_CurrentTime > TrackData.Time[m_CurrentWaypointIndex + 1])
            {
                m_CurrentWaypointIndex++;
            }

            if (m_CurrentWaypointIndex >= TrackData.Waypoints.Length - 1)
            {
                m_Transform.position = TrackData.Waypoints[^1];
                return;
            }

            var currentTime = TrackData.Time[m_CurrentWaypointIndex];
            var nextTime = TrackData.Time[m_CurrentWaypointIndex + 1];
            var waypointIntervalTime = nextTime - currentTime;

            if (waypointIntervalTime <= 0f)
            {
                m_Transform.position = TrackData.Waypoints[m_CurrentWaypointIndex];
                return;
            }

            var timeOnInterval = m_CurrentTime - currentTime;
            var t = Mathf.Clamp01(timeOnInterval / waypointIntervalTime);

            m_Transform.position = Vector3.Lerp(
                TrackData.Waypoints[m_CurrentWaypointIndex],
                TrackData.Waypoints[m_CurrentWaypointIndex + 1],
                t);
        }

        void ITrackFollower.SetTrack(TrackData trackData)
        {
            TrackData = trackData;
            m_CurrentTime = 0;
            m_CurrentWaypointIndex = 0;
        }

        // public class Factory : PlaceholderFactory<ITrackFollower>
        // {
        // }

        public class Pool : MemoryPool<ITrackFollower>
        {
        }
    }
}
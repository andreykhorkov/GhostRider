using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class TrackFollower : ITrackFollower, ITickable
    {
        private float m_CurrentTime;
        private int m_CurrentWaypointIndex;

        private readonly Transform m_Transform;

        public TrackData TrackData { get; private set; }

        public TrackFollower(Transform transform)
        {
            m_Transform = transform;
        }

        void ITickable.Tick()
        {
            if (ReferenceEquals(TrackData.Waypoints, null))
            {
                return;
            }

            if (m_CurrentWaypointIndex == TrackData.Waypoints.Length)
            {
                return;
            }

            m_CurrentTime += Time.deltaTime;

            if (m_CurrentTime > TrackData.Time[m_CurrentWaypointIndex + 1])
            {
                m_CurrentWaypointIndex++;
            }

            var nextWayPointTime = TrackData.Time[m_CurrentWaypointIndex + 1];
            var timeOnInterval = m_CurrentTime - TrackData.Time[m_CurrentWaypointIndex];
            var waypointIntervalTime = nextWayPointTime - TrackData.Time[m_CurrentWaypointIndex];
            var t = timeOnInterval / waypointIntervalTime;
            m_Transform.position = Vector3.Lerp(TrackData.Waypoints[m_CurrentWaypointIndex],
                TrackData.Waypoints[m_CurrentWaypointIndex + 1], t);
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
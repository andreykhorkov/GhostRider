using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class DebugSelfTrackFollower : ITrackFollower, ITickable
    {
        private readonly ITrackRenderer m_TrackRenderer;

        private TrackData m_TrackData;
        private int m_CurrentWaypointIndex;
        private Vector3 m_CurrentWayPointPos;
        private Vector3 m_NextWayPointPos;
        private Vector3 m_Position;
        private GameObject m_Visual;

        private ITrackFollower m_Ghost;

        public Vector3 Position => m_Position;

        TrackData ITrackFollower.TrackData => m_TrackData;

        public DebugSelfTrackFollower(ITrackRenderer trackRenderer)
        {
            m_TrackRenderer = trackRenderer;
            m_Visual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        void ITrackFollower.SetTrack(TrackData trackData)
        {
            m_TrackData = trackData;
            m_CurrentWaypointIndex = 0;

            m_TrackRenderer.SetTrackData(trackData);
            m_TrackRenderer.UpdateVisiblePath(m_CurrentWaypointIndex);
            m_Position = trackData.Waypoints[m_CurrentWaypointIndex];

        }

        void ITrackFollower.SetGhost(ITrackFollower ghost)
        {
            m_Ghost = ghost;
        }

        void ITickable.Tick()
        {
            if (ReferenceEquals(m_TrackData.Waypoints, null))
            {
                return;
            }

            m_Position = Vector3.MoveTowards(m_Position, m_Ghost.Position,
                (m_Position - m_Ghost.Position).magnitude * Time.deltaTime);

            m_Visual.transform.position = m_Position;

            const float hysteresis = 0.85f;

            while (m_CurrentWaypointIndex < m_TrackData.Waypoints.Length - 1)
            {
                var sqrDistToNext = Vector3.SqrMagnitude(m_TrackData.Waypoints[m_CurrentWaypointIndex + 1] - m_Position);
                var sqrDistToCur = Vector3.SqrMagnitude(m_TrackData.Waypoints[m_CurrentWaypointIndex] - m_Position);

                if (sqrDistToNext < sqrDistToCur * hysteresis)
                {
                    m_CurrentWaypointIndex++;
                    m_TrackRenderer.UpdateVisiblePath(m_CurrentWaypointIndex - 1);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
using DefaultNamespace.CoordinatesConverter;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class SelfTrackFollower : ITrackFollower, ITickable
    {
        private readonly IGeoCoordinatesConverter m_CoordinatesConverter;
        private readonly ITrackRenderer m_TrackRenderer;

        private TrackData m_TrackData;
        private int m_CurrentWaypointIndex;
        private Vector3 m_CurrentWayPointPos;
        private Vector3 m_NextWayPointPos;

        TrackData ITrackFollower.TrackData => m_TrackData;

        public SelfTrackFollower(IGeoCoordinatesConverter coordinatesConverter, ITrackRenderer trackRenderer)
        {
            m_CoordinatesConverter = coordinatesConverter;
            m_TrackRenderer = trackRenderer;
        }

        void ITrackFollower.SetTrack(TrackData trackData)
        {
            m_TrackData = trackData;
            m_CurrentWaypointIndex = 0;

            m_TrackRenderer.SetTrackData(trackData);
            m_TrackRenderer.UpdateVisiblePath(m_CurrentWaypointIndex);
        }

        void ITickable.Tick()
        {
            if (ReferenceEquals(m_TrackData.Waypoints, null))
            {
                return;
            }

            // if (Input.location.status != LocationServiceStatus.Running)
            // {
            //     return;
            // }
            //
            // if (Input.location.lastData.horizontalAccuracy > 20f)
            // {
            //     return;
            // }
            //
            // var lat = Input.location.lastData.latitude;
            // var lon = Input.location.lastData.longitude;
            // var alt = Input.location.lastData.altitude;
            // var selfPosition = m_CoordinatesConverter.LatLonAltToMeters(
            //     lat, lon, alt,
            //     m_TrackData.Origin.Lat,
            //     m_TrackData.Origin.Lon,
            //     m_TrackData.Origin.Alt);
            var selfPosition = Camera.main.transform.position;

            const float hysteresis = 0.85f;

            while (m_CurrentWaypointIndex < m_TrackData.Waypoints.Length - 1)
            {
                var sqrDistToNext = Vector3.SqrMagnitude(m_TrackData.Waypoints[m_CurrentWaypointIndex + 1] - selfPosition);
                var sqrDistToCur = Vector3.SqrMagnitude(m_TrackData.Waypoints[m_CurrentWaypointIndex] - selfPosition);
                //Debug.Log($"{m_TrackData.Waypoints[m_CurrentWaypointIndex + 1]}, {m_TrackData.Waypoints[m_CurrentWaypointIndex]}");

                if (sqrDistToNext < sqrDistToCur * hysteresis)
                {
                    m_CurrentWaypointIndex++;
                    Debug.Log(m_CurrentWaypointIndex);
                    m_TrackRenderer.UpdateVisiblePath(m_CurrentWaypointIndex);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
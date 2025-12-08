using DefaultNamespace.CoordinatesConverter;
using MiscTools;
using Models;
using Unity.Collections;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class TrackRenderer : ITrackRenderer, ITickable
    {
        private readonly IGeoCoordinatesConverter m_CoordinatesConverter;
        private readonly LineRenderer m_LineRenderer;
        private readonly Dispatcher m_Dispatcher;

        TrackRenderer(IGeoCoordinatesConverter coordinatesConverter, LineRenderer lineRenderer, Dispatcher dispatcher)
        {
            m_CoordinatesConverter = coordinatesConverter;
            m_LineRenderer = lineRenderer;
            m_Dispatcher = dispatcher;
        }

        void ITickable.Tick()
        {
        }

        public void CreateTrack(GeoData[] geoDataPoints)
        {
            var count = Mathf.Min(geoDataPoints.Length, 200);
            var origin = geoDataPoints[0];
            var waypoints = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                var geoData = geoDataPoints[i];
                var waypointPos = m_CoordinatesConverter.LatLonAltToMeters(geoData.Lat, geoData.Lon, geoData.Alt,
                    origin.Lat, origin.Lon, origin.Alt);
                waypoints[i] = waypointPos;
            }

            m_LineRenderer.positionCount = count;
            m_LineRenderer.SetPositions(waypoints);
            m_Dispatcher.Send(EventId.ActivityTrackCreated, System.EventArgs.Empty);
        }
    }
}
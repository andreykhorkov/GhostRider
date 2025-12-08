using DefaultNamespace.CoordinatesConverter;
using Models;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class TrackRenderer : ITrackRenderer, ITickable
    {
        private readonly IGeoCoordinatesConverter m_CoordinatesConverter;
        private readonly Material m_WaypointMaterial;

        TrackRenderer(IGeoCoordinatesConverter coordinatesConverter, Material waypointMaterial)
        {
            m_CoordinatesConverter = coordinatesConverter;
            m_WaypointMaterial = waypointMaterial;
        }

        void ITickable.Tick()
        {
        }

        public void CreateTrack(GeoData[] geoDataPoints)
        {
            var count = Mathf.Min(geoDataPoints.Length, 200);
            var origin = geoDataPoints[0];
            var scale = new Vector3(1, 1, 1);

            for (int i = 0; i < count; i++)
            {
                var geoData = geoDataPoints[i];
                var waypointPos = m_CoordinatesConverter.LatLonAltToMeters(geoData.Lat, geoData.Lon, geoData.Alt,
                    origin.Lat, origin.Lon, origin.Alt);
                var waypoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                waypoint.transform.position = waypointPos;
                waypoint.transform.localScale = scale;
                waypoint.GetComponent<Renderer>().material = m_WaypointMaterial;
            }
        }
    }
}
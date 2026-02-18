using DefaultNamespace.CoordinatesConverter;
using UnityEngine;

namespace DefaultNamespace.Track
{
    public class StravaTrackCreator : ITrackCreator
    {
        private readonly IGeoCoordinatesConverter m_CoordinatesConverter;
        private readonly ICompass m_Compass;

        public StravaTrackCreator(IGeoCoordinatesConverter coordinatesConverter, ICompass compass)
        {
            m_CoordinatesConverter = coordinatesConverter;
            m_Compass = compass;
        }

        TrackData ITrackCreator.CreateTrack(Models.TrackData[] geoDataPoints)
        {
            var origin = geoDataPoints[0];
            var waypoints = new Vector3[geoDataPoints.Length];
            var times = new int[geoDataPoints.Length];

            for (int i = 0; i < geoDataPoints.Length; i++)
            {
                var geoData = geoDataPoints[i];
                var waypointPos = m_CoordinatesConverter.LatLonAltToMeters(geoData.Lat, geoData.Lon, geoData.Alt,
                    origin.Lat, origin.Lon, origin.Alt);
                waypoints[i] = waypointPos;
                times[i] = geoData.Time;
            }

            PointTransformer.TransformPoints(waypoints, m_Compass.NorthDirection);

            return new TrackData(waypoints, times, origin);
        }
    }
}
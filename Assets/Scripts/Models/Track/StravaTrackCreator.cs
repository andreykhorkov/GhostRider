using DefaultNamespace.CoordinatesConverter;
using UnityEngine;

namespace DefaultNamespace.Track
{
    public class StravaTrackCreator : ITrackCreator
    {
        private readonly IGeoCoordinatesConverter m_CoordinatesConverter;

        public StravaTrackCreator(IGeoCoordinatesConverter coordinatesConverter)
        {
            m_CoordinatesConverter = coordinatesConverter;
        }

        TrackData ITrackCreator.CreateTrack(Models.TrackData[] geoDataPoints)
        {
            var count = Mathf.Min(geoDataPoints.Length, 200);
            var origin = geoDataPoints[0];
            var waypoints = new Vector3[count];
            var times = new int[count];

            for (int i = 0; i < count; i++)
            {
                var geoData = geoDataPoints[i];
                var waypointPos = m_CoordinatesConverter.LatLonAltToMeters(geoData.Lat, geoData.Lon, geoData.Alt,
                    origin.Lat, origin.Lon, origin.Alt);
                waypoints[i] = waypointPos;
                times[i] = geoData.Time;
            }

            return new TrackData(waypoints, times);
        }
    }
}
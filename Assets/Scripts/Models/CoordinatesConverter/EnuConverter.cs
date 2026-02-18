using System;
using UnityEngine;

namespace DefaultNamespace.CoordinatesConverter
{
    public class EnuConverter : IGeoCoordinatesConverter
    {
        Vector3 IGeoCoordinatesConverter.LatLonAltToMeters(double lat, double lon, double alt, double originLat,
            double originLon, double originAlt)
        {
            var R = 6378137.0; // Earth radius

            var dLat = (lat - originLat) * Mathf.Deg2Rad;
            var dLon = (lon - originLon) * Mathf.Deg2Rad;

            var x = dLon * R * Math.Cos(originLat * Mathf.Deg2Rad);     // East, 1 at equator, 0 at poles
            var z = dLat * R;                                           // North
            var y = alt - originAlt;                                    // Up

            return new Vector3((float)x, (float)y, (float)z);
        }
    }
}
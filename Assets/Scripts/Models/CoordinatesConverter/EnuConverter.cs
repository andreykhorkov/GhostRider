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

            var dLat = (lat - originLat) * Math.PI / 180.0;
            var dLon = (lon - originLon) * Math.PI / 180.0;

            var x = dLon * R * Math.Cos(originLat * Math.PI / 180.0);   // East
            var z = dLat * R;                                           // North
            var y = alt - originAlt;                                    // Up

            return new Vector3((float)x, (float)y, (float)z);
        }

    }
}
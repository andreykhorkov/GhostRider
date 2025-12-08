using UnityEngine;

namespace DefaultNamespace.CoordinatesConverter
{
    public interface IGeoCoordinatesConverter
    {
        Vector3 LatLonAltToMeters(double lat, double lon, double alt, double originLat, double originLon,
            double originAlt);
    }
}
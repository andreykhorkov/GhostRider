using UnityEngine;

namespace Models
{
    public struct GeoData
    {
        public Vector3 LatLngAlt { get; }
        public int Time { get; }

        public GeoData(Vector3 latLngAlt, int time)
        {
            LatLngAlt = latLngAlt;
            Time = time;
        }

        public override string ToString()
        {
            return $"{LatLngAlt}, {Time}";
        }
    }
}
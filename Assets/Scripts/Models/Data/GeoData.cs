using UnityEngine;

namespace Models
{
    public struct GeoData
    {
        public double Lat { get; }
        public double Lon { get; }
        public double Alt { get; }
        public int Time { get; }

        public GeoData(double lat, double lon, double alt, int time)
        {
            Lat = lat;
            Lon = lon;
            Alt = alt;
            Time = time;
        }

        public override string ToString()
        {
            return $"{Lat:00},{Lon:00},{Alt:00}, {Time}";
        }
    }
}
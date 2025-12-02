using System;
using System.Collections.Generic;

public static class PolylineDecoder
{
    public static List<(double Lat, double Lng)> Decode(string polyline)
    {
        var coordinates = new List<(double Lat, double Lng)>();
        int index = 0, len = polyline.Length;
        int lat = 0, lng = 0;

        while (index < len)
        {
            int b, shift = 0, result = 0;
            do
            {
                b = polyline[index++] - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            } while (b >= 0x20);

            int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
            lat += dlat;

            shift = 0;
            result = 0;
            do
            {
                b = polyline[index++] - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            } while (b >= 0x20);

            int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
            lng += dlng;

            coordinates.Add((lat / 1e5, lng / 1e5));
        }

        return coordinates;
    }
}
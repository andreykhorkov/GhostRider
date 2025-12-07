using UnityEngine;

namespace Models
{
    public interface IDataProvider
    {
        Awaitable<GeoData[]> GetActivityGeoData(string url, string token);
        Awaitable<long[]> GetActivityIds(string url, string token);
    }
}
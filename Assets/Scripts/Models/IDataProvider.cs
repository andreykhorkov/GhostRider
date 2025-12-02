using UnityEngine;

namespace Models
{
    public interface IDataProvider
    {
        Awaitable<GeoData[]> GetActivityGeoData();
        Awaitable<long[]> GetActivityIds();
    }
}
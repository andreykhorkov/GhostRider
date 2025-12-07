using DefaultNamespace.UI;
using UnityEngine;

namespace Models
{
    public interface IDataProvider
    {
        Awaitable<GeoData[]> GetActivityGeoData(string url, string token);
        Awaitable<ActivityAttributes[]> GetActivitiesAttributes(string url, string token);
    }
}
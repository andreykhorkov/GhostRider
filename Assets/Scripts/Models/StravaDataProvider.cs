using System.Collections.Generic;
using DefaultNamespace;
using Newtonsoft.Json;
using StravaData;
using UnityEngine;

namespace Models
{
    public class StravaDataProvider : IDataProvider
    {
        private readonly IActivityLoader m_ActivityLoader;

        public StravaDataProvider(IActivityLoader activityLoader)
        {
            m_ActivityLoader = activityLoader;
        }

        public async Awaitable<long[]> GetActivityIds()
        {
            var activitiesJson = await m_ActivityLoader.GetActivityData("activities");
            var stravaActivities = JsonConvert.DeserializeObject<List<StravaActivity>>(activitiesJson);
            var IDs = new long[stravaActivities.Count];

            for (int i = 0; i < stravaActivities.Count; i++)
            {
                IDs[i] = stravaActivities[i].Id;
            }

            return IDs;
        }

        async Awaitable<GeoData[]> IDataProvider.GetActivityGeoData()
        {
            var streamsJson = await m_ActivityLoader.GetActivityData("activity_streams");
            var streams = JsonConvert.DeserializeObject<ActivityStreams>(streamsJson);

            if (streams.LatLng.OriginalSize != streams.Altitude.OriginalSize
                && streams.LatLng.OriginalSize != streams.Time.OriginalSize)
            {
                Debug.LogError("sds");
                return null;
            }

            var geoData = new GeoData[streams.LatLng.OriginalSize];

            for (int i = 0; i < streams.LatLng.OriginalSize; i++)
            {
                var pos = new Vector3(
                    (float)streams.LatLng.Data[i][0],
                    (float)streams.LatLng.Data[i][1],
                    streams.Altitude.Data[i]);
                var time = streams.Time.Data[i];
                geoData[i] = new GeoData(pos, time);
            }

            return geoData;
        }
    }
}
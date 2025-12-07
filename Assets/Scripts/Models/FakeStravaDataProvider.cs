using System.Collections.Generic;
using DefaultNamespace;
using Newtonsoft.Json;
using StravaData;
using UnityEngine;

namespace Models
{
    public class FakeStravaDataProvider : IDataProvider
    {
        private static string GetFakeData(string path)
        {
            return Resources.Load<TextAsset>(path).text;
        }

        public async Awaitable<long[]> GetActivityIds(string url, string token)
        {
            var activitiesJson = GetFakeData("activities");
            var stravaActivities = JsonConvert.DeserializeObject<List<StravaActivity>>(activitiesJson);
            var IDs = new long[stravaActivities.Count];

            for (int i = 0; i < stravaActivities.Count; i++)
            {
                IDs[i] = stravaActivities[i].Id;
            }

            return IDs;
        }

        async Awaitable<GeoData[]> IDataProvider.GetActivityGeoData(string url, string token)
        {
            var streamsJson = GetFakeData("activity_streams");
            var streams = JsonConvert.DeserializeObject<ActivityStreams>(streamsJson);

            if (streams.LatLng.OriginalSize != streams.Altitude.OriginalSize
                && streams.LatLng.OriginalSize != streams.Time.OriginalSize)
            {
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
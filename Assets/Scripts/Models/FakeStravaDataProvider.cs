using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.UI;
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

        public async Awaitable<ActivityAttributes[]> GetActivitiesAttributes(string url, string token)
        {
            var activitiesJson = GetFakeData("activities");

            try
            {
                var stravaActivities = JsonConvert.DeserializeObject<List<StravaActivity>>(activitiesJson);
                var activityAttributes = new ActivityAttributes[stravaActivities.Count];

                for (int i = 0; i < stravaActivities.Count; i++)
                {
                    var activity =
                        stravaActivities
                            [i]; //string date, string name, string distance, string elapsed, string location
                    activityAttributes[i] = new ActivityAttributes(activity.Id, activity.StartDate, activity.Name,
                        activity.Distance, activity.ElapsedTime, "Los Altos Hills");
                }

                return activityAttributes;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
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
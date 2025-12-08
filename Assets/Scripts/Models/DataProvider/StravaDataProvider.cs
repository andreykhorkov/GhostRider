using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.UI;
using Newtonsoft.Json;
using StravaData;
using UnityEngine;

namespace Models
{
    public class StravaDataProvider : IDataProvider
    {
        private readonly IActivityLoader m_StravaActivityDataLoader;

        public StravaDataProvider(IActivityLoader stravaActivityDataLoader)
        {
            m_StravaActivityDataLoader = stravaActivityDataLoader;
        }

        async Awaitable<GeoData[]> IDataProvider.GetActivityGeoData(string url, string token)
        {
            var streamsJson = await m_StravaActivityDataLoader.GetActivityData(
                url, new Tuple<string, string>[] { new("Authorization", $"Bearer {token}") });
            var streams = JsonConvert.DeserializeObject<ActivityStreams>(streamsJson);

            if (streams.LatLng.OriginalSize != streams.Altitude.OriginalSize
                && streams.LatLng.OriginalSize != streams.Time.OriginalSize)
            {
                return null;
            }

            var geoData = new GeoData[streams.LatLng.OriginalSize];

            for (int i = 0; i < streams.LatLng.OriginalSize; i++)
            {
                var lat = streams.LatLng.Data[i][0];
                var lon = streams.LatLng.Data[i][1];
                var alt = streams.Altitude.Data[i];
                var time = streams.Time.Data[i];
                geoData[i] = new GeoData(lat, lon, alt, time);
            }

            return geoData;
        }

        async Awaitable<ActivityAttributes[]> IDataProvider.GetActivitiesAttributes(string url, string token)
        {
            var activitiesJson = await m_StravaActivityDataLoader.GetActivityData(
                url, new Tuple<string, string>[] { new("Authorization", $"Bearer {token}") });
            try
            {
                var stravaActivities = JsonConvert.DeserializeObject<List<StravaActivity>>(activitiesJson);
                var activityAttributes = new ActivityAttributes[stravaActivities.Count];

                for (int i = 0; i < stravaActivities.Count; i++)
                {
                    var activity = stravaActivities[i];
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
    }
}
using System;
using System.Text;
using System.Text.RegularExpressions;
using Models;
using Newtonsoft.Json;
using StravaData;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GhostRiderFacade : IInitializable, IDisposable
    {
        private readonly IActivityLoader m_ActivityLoader;
        private readonly IAthenticator m_Authenticator;
        private readonly IDataProvider m_DataProvider;

        private const string m_ClientId = "185672";

        private string m_AccessToken;

        public GhostRiderFacade(IActivityLoader activityLoader, IAthenticator athenticator, IDataProvider dataProvider)
        {
            m_ActivityLoader = activityLoader;
            m_Authenticator = athenticator;
            m_DataProvider = dataProvider;
        }

        void IInitializable.Initialize()
        {
            Init("asfafa");
            return;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                OnDeepLinkActivated(Application.absoluteURL);
            }
            else
            {
                m_Authenticator.Authorize(m_ClientId);
            }

            Application.deepLinkActivated += OnDeepLinkActivated;
        }

        void IDisposable.Dispose()
        {
            Application.deepLinkActivated -= OnDeepLinkActivated;
        }

        void OnDeepLinkActivated(string info)
        {
            var match = Regex.Match(info, @"[?&]code=([^&]+)");

            if (match.Success)
            {
                var code = match.Groups[1].Value;
                _ = Init(code);
            }
        }

        private async Awaitable Init(string code)
        {
            //m_AccessToken = await m_Authenticator.ExchangeCodeForToken(code);

            //
            // var activitiesJson = await m_ActivityLoader.GetActivityData(
            //     "https://www.strava.com/api/v3/athlete/activities?page=1&per_page=1",
            //     new Tuple<string, string>[]{ new("Authorization", $"Bearer {m_AccessToken}") });

            var Ids = await m_DataProvider.GetActivityIds();

            var sbb = new StringBuilder();

            foreach (var id in Ids)
            {
                sbb.Append($"{id}, ");
            }

            Debug.Log(sbb);


            var activityGeoData = await m_DataProvider.GetActivityGeoData();

            var sb = new StringBuilder();

            foreach (var data in activityGeoData)
            {
                sb.Append($"{data}, ");
            }

            Debug.Log(sb);
        }
    }
}
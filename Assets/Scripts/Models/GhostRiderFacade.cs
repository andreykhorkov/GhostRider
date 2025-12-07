using System;
using System.Text;
using Models;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GhostRiderFacade : IInitializable, IDisposable
    {
        private readonly IAuthenticator m_Authenticator;
        private readonly IDataProvider m_DataProvider;

        private const string m_ClientId = "185672";

        private string m_AccessToken;

        public GhostRiderFacade(IAuthenticator authenticator, IDataProvider dataProvider)
        {
            m_Authenticator = authenticator;
            m_DataProvider = dataProvider;
        }

        void IInitializable.Initialize()
        {
            if (!string.IsNullOrEmpty(m_Authenticator.AbsoluteUrl))
            {
                OnDeepLinkActivated(m_Authenticator.AbsoluteUrl);
                Init();
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

        private async void OnDeepLinkActivated(string path)
        {
            var code = m_Authenticator.RetrieveExchangeCode();
            m_AccessToken = await m_Authenticator.ExchangeCodeForToken(code);
        }

        private async Awaitable Init()
        {
            var Ids = await m_DataProvider.GetActivityIds(
                "https://www.strava.com/api/v3/athlete/activities?page=1&per_page=1", m_AccessToken);

            var sbb = new StringBuilder();

            foreach (var id in Ids)
            {
                sbb.Append($"{id}, ");
            }

            Debug.Log(sbb);


            var activityGeoData = await m_DataProvider.GetActivityGeoData("", m_AccessToken);

            var sb = new StringBuilder();

            foreach (var data in activityGeoData)
            {
                sb.Append($"{data}, ");
            }

            Debug.Log(sb);
        }
    }
}
using System;
using System.Text;
using DefaultNamespace.EventArgs;
using DefaultNamespace.Track;
using MiscTools;
using Models;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GhostRiderFacade : IInitializable, IDisposable
    {
        private readonly Dispatcher m_Dispatcher;
        private readonly IAuthenticator m_Authenticator;
        private readonly IDataProvider m_DataProvider;
        private readonly ITrackRenderer m_TrackRenderer;
        private readonly Material m_WaypointMaterial;

        private const string m_ClientId = "185672";

        private string m_AccessToken;

        public GhostRiderFacade(Dispatcher dispatcher, IAuthenticator authenticator, IDataProvider dataProvider,
            ITrackRenderer trackRenderer)
        {
            m_Dispatcher = dispatcher;
            m_Authenticator = authenticator;
            m_DataProvider = dataProvider;
            m_TrackRenderer = trackRenderer;
        }

        void IInitializable.Initialize()
        {
            if (!string.IsNullOrEmpty(m_Authenticator.AbsoluteUrl))
            {
                OnDeepLinkActivated(m_Authenticator.AbsoluteUrl);
                GetActivities();
            }
            else
            {
                m_Authenticator.Authorize(m_ClientId);
            }

            Application.deepLinkActivated += OnDeepLinkActivated;
            m_Dispatcher.Subscribe(EventId.ActivityLoadClicked, OnActivityLoadClicked);
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

        private void OnActivityLoadClicked(System.EventArgs args)
        {
            var eventArgs = (LoadActivityEventArgs)args;
            var activityId = eventArgs.ActivityId;
            LoadActivityInfo(activityId);
        }

        private async Awaitable LoadActivityInfo(long activityId)
        {
            var activityGeoData = await m_DataProvider.GetActivityGeoData($"{activityId}", m_AccessToken);
            var sb = new StringBuilder();

            foreach (var data in activityGeoData)
            {
                sb.Append($"{data}, ");
            }

            Debug.Log(sb);

            m_TrackRenderer.CreateTrack(activityGeoData);
        }

        private async Awaitable GetActivities()
        {
            var activitiesAttributes = await m_DataProvider.GetActivitiesAttributes(
                "https://www.strava.com/api/v3/athlete/activities?page=1&per_page=1", m_AccessToken);
            m_Dispatcher.Send(EventId.ActivityIdsRetrieved, new ActivitiesAttributesEventArgs(activitiesAttributes));

            var sbb = new StringBuilder();

            foreach (var id in activitiesAttributes)
            {
                sbb.Append($"{id}, ");
            }

            Debug.Log(sbb);
        }
    }
}
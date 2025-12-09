using System;
using System.Text;
using DefaultNamespace.EventArgs;
using DefaultNamespace.Track;
using MiscTools;
using Models;
using UnityEngine;
using Zenject;
using TrackData = Models.TrackData;

namespace DefaultNamespace
{
    public class GhostRiderFacade : IInitializable, IDisposable
    {
        private readonly Dispatcher m_Dispatcher;
        private readonly IAuthenticator m_Authenticator;
        private readonly IDataProvider m_DataProvider;
        private readonly ITrackRenderer m_TrackRenderer;
        private readonly ITrackCreator m_trackCreator;
        private readonly Material m_WaypointMaterial;
        private readonly TrackFollower.Factory m_TrackFollowerFactory;

        private const string m_ClientId = "185672";

        private string m_AccessToken;

        public GhostRiderFacade(Dispatcher dispatcher, IAuthenticator authenticator, IDataProvider dataProvider,
            ITrackRenderer trackRenderer, ITrackCreator trackCreator, TrackFollower.Factory trackFollowerFactory)
        {
            m_Dispatcher = dispatcher;
            m_Authenticator = authenticator;
            m_DataProvider = dataProvider;
            m_TrackRenderer = trackRenderer;
            m_trackCreator = trackCreator;
            m_TrackFollowerFactory = trackFollowerFactory;
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

        private async void OnActivityLoadClicked(System.EventArgs args)
        {
            var eventArgs = (LoadActivityEventArgs)args;
            var activityId = eventArgs.ActivityId;
            var activityGeoData = await LoadActivityInfo(activityId);
            var trackData = m_trackCreator.CreateTrack(activityGeoData);
            m_TrackRenderer.CreateTrackTrace(trackData);
            m_Dispatcher.Send(EventId.ActivityTrackCreated, System.EventArgs.Empty);
            ITrackFollower ghost = m_TrackFollowerFactory.Create();
            ghost.SetTrack(trackData);
        }

        private async Awaitable<TrackData[]> LoadActivityInfo(long activityId)
        {
            return await m_DataProvider.GetActivityGeoData($"{activityId}", m_AccessToken);
        }

        private async Awaitable GetActivities()
        {
            var activitiesAttributes = await m_DataProvider.GetActivitiesAttributes(
                "https://www.strava.com/api/v3/athlete/activities?page=1&per_page=1", m_AccessToken);
            m_Dispatcher.Send(EventId.ActivityIdsRetrieved, new ActivitiesAttributesEventArgs(activitiesAttributes));
        }
    }
}
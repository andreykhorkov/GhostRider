using MiscTools;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class TrackRenderer : ITrackRenderer, ITickable
    {

        private readonly LineRenderer m_LineRenderer;
        private readonly Dispatcher m_Dispatcher;

        TrackRenderer(MainInstaller.Installables installables, Dispatcher dispatcher)
        {
            m_LineRenderer = installables.m_LineRenderer;
            m_Dispatcher = dispatcher;
        }

        void ITickable.Tick()
        {
        }

        public void UpdateTrackTrace(TrackData trackData)
        {
            m_LineRenderer.positionCount = trackData.Waypoints.Length;
            m_LineRenderer.SetPositions(trackData.Waypoints);
        }
    }
}
using MiscTools;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class TrackRenderer : ITrackRenderer, ITickable
    {

        private readonly LineRenderer m_LineRenderer;
        private readonly Dispatcher m_Dispatcher;

        TrackRenderer(LineRenderer lineRenderer, Dispatcher dispatcher)
        {
            m_LineRenderer = lineRenderer;
            m_Dispatcher = dispatcher;
        }

        void ITickable.Tick()
        {
        }

        public void CreateTrackTrace(TrackData trackData)
        {
            m_LineRenderer.positionCount = trackData.Waypoints.Length;
            m_LineRenderer.SetPositions(trackData.Waypoints);
        }
    }
}
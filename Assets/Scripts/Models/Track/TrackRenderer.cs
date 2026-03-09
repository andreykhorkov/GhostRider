using MiscTools;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class TrackRenderer : ITrackRenderer, ITickable
    {
        public const int k_VisiblePathLength = 10;

        private readonly LineRenderer m_LineRenderer;
        private readonly Dispatcher m_Dispatcher;
        private readonly Vector3[] m_PathSubset = new Vector3[k_VisiblePathLength];
        private readonly ICompass m_Compass;

        private TrackData m_TrackData;

        private TrackRenderer(MainInstaller.Installables installables, Dispatcher dispatcher, ICompass compass)
        {
            m_LineRenderer = installables.m_LineRenderer;
            m_Dispatcher = dispatcher;
            m_Compass = compass;
        }

        void ITickable.Tick()
        {
            m_LineRenderer.transform.rotation = Quaternion.LookRotation(m_Compass.NorthDirection);
        }

        void ITrackRenderer.UpdateVisiblePath(int startIndex)
        {
            if (startIndex > m_TrackData.Waypoints.Length - k_VisiblePathLength)
            {
                return;
            }

            for (var i = 0; i < k_VisiblePathLength; i++)
            {
                var point = m_TrackData.Waypoints[startIndex + i];
                PointTransformer.TransformPoint(ref point, m_Compass.NorthDirection);
                m_PathSubset[i] = point;
            }

            m_LineRenderer.SetPositions(m_PathSubset);
        }

        void ITrackRenderer.SetTrackData(TrackData trackData)
        {
            m_LineRenderer.positionCount = k_VisiblePathLength;
            m_TrackData = trackData;
        }
    }
}
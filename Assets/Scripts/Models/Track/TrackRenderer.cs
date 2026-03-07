using System.Collections.Generic;
using System.Diagnostics;
using MiscTools;
using UnityEngine;
using UnityEngine.Profiling;
using Zenject;
using Debug = UnityEngine.Debug;

namespace DefaultNamespace.Track
{
    public class TrackRenderer : ITrackRenderer, ITickable
    {
        public const int k_VisiblePathLength = 10;

        private readonly LineRenderer m_LineRenderer;
        private readonly Dispatcher m_Dispatcher;
        private readonly Vector3[] m_PathSubset = new Vector3[k_VisiblePathLength];

        private TrackData m_TrackData;

        TrackRenderer(MainInstaller.Installables installables, Dispatcher dispatcher)
        {
            m_LineRenderer = installables.m_LineRenderer;
            m_Dispatcher = dispatcher;
        }

        void ITickable.Tick()
        {
        }

        private Stopwatch sw = new Stopwatch();

        void ITrackRenderer.UpdateVisiblePath(int startIndex)
        {
            sw.Restart();
            if (startIndex > m_TrackData.Waypoints.Length - k_VisiblePathLength)
            {
                return;
            }

            for (var i = 0; i < k_VisiblePathLength; i++)
            {
                m_PathSubset[i] = m_TrackData.Waypoints[startIndex + i];
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
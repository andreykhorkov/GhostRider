using UnityEngine;

namespace DefaultNamespace.Track
{
    public struct TrackData
    {
        public Vector3[] Waypoints { get; }
        public int[] Time { get; }

        public TrackData(Vector3[] waypoints, int[] time)
        {
            Waypoints = waypoints;
            Time = time;
        }
    }
}
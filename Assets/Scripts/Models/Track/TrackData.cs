using UnityEngine;

namespace DefaultNamespace.Track
{
    public struct TrackData
    {
        public Vector3[] Waypoints { get; }
        public int[] Time { get; }
        public Models.TrackData Origin { get; }

        public TrackData(Vector3[] waypoints, int[] time, Models.TrackData origin)
        {
            Waypoints = waypoints;
            Time = time;
            Origin = origin;
        }
    }
}
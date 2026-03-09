using UnityEngine;

namespace DefaultNamespace.Track
{
    public interface ITrackFollower
    {
        TrackData TrackData { get; }
        void SetTrack(TrackData trackData);
        void SetGhost(ITrackFollower ghost);
        Vector3 Position { get; }
    }
}
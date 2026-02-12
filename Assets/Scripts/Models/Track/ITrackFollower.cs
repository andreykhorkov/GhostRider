using System;
using Zenject;

namespace DefaultNamespace.Track
{
    public interface ITrackFollower
    {
        TrackData TrackData { get; }
        void SetTrack(TrackData trackData);
    }
}
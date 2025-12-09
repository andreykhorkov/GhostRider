namespace DefaultNamespace.Track
{
    public interface ITrackFollower
    {
        TrackData TrackData { get; }
        void SetTrack(TrackData trackData);
    }
}
using Models;

namespace DefaultNamespace.Track
{
    public interface ITrackCreator
    {
        TrackData CreateTrack(Models.TrackData[] geoDataPoints);
    }
}
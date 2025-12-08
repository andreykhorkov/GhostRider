using Models;

namespace DefaultNamespace.Track
{
    public interface ITrackRenderer
    {
        void CreateTrack(GeoData[] geoDataPoints);
    }
}
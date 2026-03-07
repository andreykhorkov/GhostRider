using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Track
{
    public interface ITrackRenderer
    {
        void SetTrackData(TrackData trackData);
        void UpdateVisiblePath(int startIndex);
    }
}
using DefaultNamespace.CoordinatesConverter;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Track
{
    public class Test : MonoBehaviour
    {
        [Inject] private IGeoCoordinatesConverter m_Converter;
        [SerializeField] private Transform m_NorthDirection;

        private void Start()
        {
            Input.location.Start(1f, 1f);
            Input.compass.enabled = true;
        }

        void Update()
        {
            if (Input.location.status != LocationServiceStatus.Running)
            {
                return;
            }

            var lat = Input.location.lastData.latitude;
            var lon = Input.location.lastData.longitude;
            var alt = Input.location.lastData.altitude;
            // var northDirection = m_Converter.LatLonAltToMeters(lat + 0.1, lon, alt, lat, lon, alt);
            // northDirection.y = 0;
            // northDirection.Normalize();
            //
            // m_NorthDirection.forward = northDirection;

            Debug.Log($"{lat}, {lon}, {alt}");
            //37.35492, -122.1229, 112.4
        }
    }
}
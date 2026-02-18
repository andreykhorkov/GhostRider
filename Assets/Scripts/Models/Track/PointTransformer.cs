using UnityEngine;

namespace DefaultNamespace.Track
{
    public static class PointTransformer
    {
        public static void TransformPoints(Vector3[] points, Vector3 direction, Vector3 up = default)
        {
            if (up == default)
            {
                up = Vector3.up;
            }

            direction.Normalize();
            Quaternion rotation = Quaternion.LookRotation(direction, up);
            Matrix4x4 matrix = Matrix4x4.Rotate(rotation);

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = matrix.MultiplyPoint3x4(points[i]);
            }
        }
    }
}
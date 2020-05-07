using UnityEngine;

namespace HexagonalLib.Utility
{
    internal static class VectorUtility
    {
        internal static (float x, float y) AsTuple(this Vector2 vector)
        {
            return (vector.x, vector.y);
        }

        internal static (float x, float y) AsTuple(this Vector3 vector)
        {
            return (vector.x, vector.z);
        }

        internal static Vector2 AsVector2(this (float x, float y) tuple)
        {
            return new Vector2(tuple.x, tuple.y);
        }

        internal static Vector3 AsVector3(this (float x, float y) tuple, float y = 0.0f)
        {
            return new Vector3(tuple.x, y, tuple.y);
        }
    }
}
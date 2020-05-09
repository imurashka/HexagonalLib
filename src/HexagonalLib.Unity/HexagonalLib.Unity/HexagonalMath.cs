using UnityEngine;

namespace HexagonalLib
{
    public static partial class HexagonalMath
    {
        /// <summary>
        /// Compares two vectors and returns true if they are similar.
        /// </summary>
        public static bool SimilarTo(this in Vector2 a, in Vector2 b)
        {
            return a.x.SimilarTo(b.x) && a.y.SimilarTo(b.y);
        }

        /// <summary>
        /// Compares two vectors and returns true if they are similar.
        /// </summary>
        public static bool SimilarTo(this in Vector3 a, in Vector3 b)
        {
            return a.x.SimilarTo(b.x) && a.y.SimilarTo(b.y) && a.z.SimilarTo(b.z);
        }
    }
}
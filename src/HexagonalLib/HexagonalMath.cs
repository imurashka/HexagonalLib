using System;

namespace HexagonalLib
{
    public static partial class HexagonalMath
    {
        /// <summary>
        /// Rotate 2d vector around z Axis clockwise
        /// </summary>
        /// <param name="vector">Vector to rotate</param>
        /// <param name="degrees">Angle of rotation</param>
        public static (float X, float Y) Rotate(this in (float X, float Y) vector, float degrees)
        {
            var radians = Math.PI / 180.0 * degrees;
            var sin = Math.Sin(radians);
            var cos = Math.Cos(radians);
            var x = (float) Math.Round(cos * vector.X - sin * vector.Y, 6);
            var y = (float) Math.Round(sin * vector.X + cos * vector.Y, 6);
            return (-x, y);
        }

        /// <summary>
        /// Returns this vector with a magnitude of 1
        /// </summary>
        public static (float X, float Y) Normalize(this in (float X, float Y) vector)
        {
            var distance = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return ((float) (vector.X / distance), (float) (vector.Y / distance));
        }

        /// <summary>
        /// Compares two floating point values and returns true if they are similar.
        /// </summary>
        public static bool SimilarTo(this in float a, in float b)
        {
            return Math.Abs(b - a) < (double) Math.Max(1E-06f * Math.Max(Math.Abs(a), Math.Abs(b)), float.Epsilon * 8f);
        }

        /// <summary>
        /// Compares two vectors and returns true if they are similar.
        /// </summary>
        public static bool SimilarTo(this in (float X, float Y) a, in (float X, float Y)  b)
        {
            return a.X.SimilarTo(b.X) && a.Y.SimilarTo(b.Y);
        }
    }
}
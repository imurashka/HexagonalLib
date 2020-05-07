using System.Runtime.CompilerServices;

namespace HexagonalLib
{
    public static partial class HexagonalMath
    {
        /// <summary>
        /// The well-known 3.14159265358979... value
        /// </summary>
        public static float PI
        {
            get
            {
                var pi = default(float);
                GetPI(ref pi);
                return pi;
            }
        }

        /// <summary>
        /// Returns the sine of the specified angle
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(in float value)
        {
            var result = default(float);
            Sqrt(value, ref result);
            return result;
        }

        /// <summary>
        /// Returns the absolute value of value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(in float value)
        {
            var result = default(float);
            Abs(value, ref result);
            return result;
        }

        /// <summary>
        /// Returns the absolute value of value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(in int value)
        {
            var result = default(int);
            Abs(value, ref result);
            return result;
        }

        /// <summary>
        /// Returns the sine of the specified angle
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(in float radians)
        {
            var sin = default(float);
            Sin(radians, ref sin);
            return sin;
        }

        /// <summary>
        /// Returns the cosine of the specified angle
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(in float radians)
        {
            var cos = default(float);
            Cos(radians, ref cos);
            return cos;
        }

        /// <summary>
        /// Degrees to radians conversion
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Deg2Rad(in float degrees)
        {
            var radians = default(float);
            Deg2Rad(degrees, ref radians);
            return radians;
        }

        /// <summary>
        /// Radians to degrees conversion 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Rad2Deg(in float radians)
        {
            var degrees = default(float);
            Rad2Deg(radians, ref degrees);
            return degrees;
        }

        /// <summary>
        /// Rotate 2d vector around z Axis clockwise
        /// </summary>
        /// <param name="vector">Vector to rotate</param>
        /// <param name="degrees">Angle of rotation</param>
        /// <returns>Rotated vector</returns>
        public static (float X, float Y) Rotate(this (float X, float Y) vector, float degrees)
        {
            var sin = Sin(Deg2Rad(degrees));
            var cos = Cos(Deg2Rad(degrees));
            return (cos * vector.X - sin * vector.Y, sin * vector.X + cos * vector.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static partial void Sqrt(in float value, ref float result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static partial void Abs(in float value, ref float result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static partial void Abs(in int value, ref int result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static partial void GetPI(ref float pi);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static partial void Sin(in float radians, ref float sin);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static partial void Cos(in float radians, ref float cos);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static partial void Deg2Rad(in float degrees, ref float radians);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static partial void Rad2Deg(in float radians, ref float degrees);
    }
}
﻿using System;

namespace HexagonalLib
{
    public static partial class HexagonalMath
    {
        /// <summary>
        /// Degrees-to-radians conversion constant (Read Only)
        /// </summary>
        public static readonly double Deg2Rad = Math.PI / 180.0;

        /// <summary>
        /// Radians-to-degrees conversion constant (Read Only)
        /// </summary>
        public static readonly double Rad2Deg = 180.0 / Math.PI;

        /// <summary>
        /// Rotate 2d vector around z Axis clockwise
        /// </summary>
        /// <param name="vector">Vector to rotate</param>
        /// <param name="degrees">Angle of rotation</param>
        public static (float X, float Y) Rotate(this in (float X, float Y) vector, float degrees)
        {
            var sin = Math.Sin(Deg2Rad * degrees);
            var cos = Math.Cos(Deg2Rad * degrees);
            var x = (float) Math.Round(cos * vector.X - sin * vector.Y, 7);
            var y = (float) Math.Round(sin * vector.X + cos * vector.Y, 7);
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
        public static bool Approximately(this in float a, float b)
        {
            return Math.Abs(b - a) < (double) Math.Max(1E-06f * Math.Max(Math.Abs(a), Math.Abs(b)), float.Epsilon * 8f);
        }
    }
}
using UnityEngine;

namespace HexagonalLib
{
    public static partial class HexagonalMath
    {
        // ReSharper disable once RedundantAssignment
        static partial void Sqrt(in float value, ref float result)
        {
            result = Mathf.Sqrt(value);
        }
        
        // ReSharper disable once RedundantAssignment
        static partial void Abs(in float value, ref float result)
        {
            result = Mathf.Abs(value);
        }
        
        // ReSharper disable once RedundantAssignment
        static partial void Abs(in int value, ref int result)
        {
            result = Mathf.Abs(value);
        }

        // ReSharper disable once RedundantAssignment
        static partial void GetPI(ref float pi)
        {
            pi = Mathf.PI;
        }
        // ReSharper disable once RedundantAssignment
        static partial void Sin(in float radians, ref float sin)
        {
            sin = Mathf.Sin(radians);
        }

        // ReSharper disable once RedundantAssignment
        static partial void Cos(in float radians, ref float cos)
        {
            cos = Mathf.Cos(radians);
        }

        // ReSharper disable once RedundantAssignment
        static partial void Deg2Rad(in float degrees, ref float radians)
        {
            radians = Mathf.Deg2Rad * degrees;
        }

        // ReSharper disable once RedundantAssignment
        static partial void Rad2Deg(in float radians, ref float degrees)
        {
            degrees = Mathf.Rad2Deg * radians;
        }
    }
}
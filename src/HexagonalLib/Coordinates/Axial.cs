using System;
using System.Collections.Generic;

namespace HexagonalLib.Coordinates
{
    [Serializable]
    public readonly partial struct Axial : IEquatable<Axial>, IEqualityComparer<Axial>
    {
        public static Axial Zero => new Axial(0, 0);

        public readonly int Q;
        public readonly int R;

        public Axial(int q, int r)
            : this()
        {
            Q = q;
            R = r;
        }

        public static bool operator ==(Axial coord1, Axial coord2)
        {
            return (coord1.Q, coord1.R) == (coord2.Q, coord2.R);
        }

        public static bool operator !=(Axial coord1, Axial coord2)
        {
            return (coord1.Q, coord1.R) != (coord2.Q, coord2.R);
        }

        public static Axial operator +(Axial coord1, Axial coord2)
        {
            return new Axial(coord1.Q + coord2.Q, coord1.R + coord2.R);
        }

        public static Axial operator +(Axial coord, int offset)
        {
            return new Axial(coord.Q + offset, coord.R + offset);
        }

        public static Axial operator -(Axial coord1, Axial coord2)
        {
            return new Axial(coord1.Q - coord2.Q, coord1.R - coord2.R);
        }

        public static Axial operator -(Axial coord, int offset)
        {
            return new Axial(coord.Q - offset, coord.R - offset);
        }

        public static Axial operator *(Axial coord, int offset)
        {
            return new Axial(coord.Q * offset, coord.R * offset);
        }

        public override bool Equals(object other)
        {
            return other is Axial axial && Equals(axial);
        }

        public bool Equals(Axial other)
        {
            return (Q, R) == (other.Q, other.R);
        }

        public bool Equals(Axial coord1, Axial coord2)
        {
            return coord1.Equals(coord2);
        }

        public override int GetHashCode()
        {
            return (Q, R).GetHashCode();
        }

        public int GetHashCode(Axial axial)
        {
            return axial.GetHashCode();
        }

        public override string ToString()
        {
            return $"A-[{Q}:{R}]";
        }
    }
}
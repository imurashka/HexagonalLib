using System;

namespace HexagonalLib.Coordinates
{
    [Serializable]
    public readonly partial struct Cubic
    {
        public static Cubic Zero => new Cubic(0, 0, 0);

        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public Cubic(int x, int y, int z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Round float coordinates to nearest valid coordinate
        /// </summary>
        public Cubic(float x, float y, float z)
        {
            var rx = (int) Math.Round(x);
            var ry = (int) Math.Round(y);
            var rz = (int) Math.Round(z);

            var xDiff = Math.Abs(rx - x);
            var yDiff = Math.Abs(ry - y);
            var zDiff = Math.Abs(rz - z);

            if (xDiff > yDiff && xDiff > zDiff)
            {
                rx = -ry - rz;
            }
            else if (yDiff > zDiff)
            {
                ry = -rx - rz;
            }
            else
            {
                rz = -rx - ry;
            }

            X = rx;
            Y = ry;
            Z = rz;
        }

        public static bool operator ==(Cubic coord1, Cubic coord2)
        {
            return (coord1.X, coord1.Y, coord1.Z) == (coord2.X, coord2.Y, coord2.Z);
        }

        public static bool operator !=(Cubic coord1, Cubic coord2)
        {
            return (coord1.X, coord1.Y, coord1.Z) != (coord2.X, coord2.Y, coord2.Z);
        }

        public static Cubic operator +(Cubic coord1, Cubic coord2)
        {
            return new Cubic(coord1.X + coord2.X, coord1.Y + coord2.Y, coord1.Z + coord2.Z);
        }

        public static Cubic operator +(Cubic coord, int offset)
        {
            return new Cubic(coord.X + offset, coord.Y + offset, coord.Z + offset);
        }

        public static Cubic operator -(Cubic coord1, Cubic coord2)
        {
            return new Cubic(coord1.X - coord2.X, coord1.Y - coord2.Y, coord1.Z - coord2.Z);
        }

        public static Cubic operator -(Cubic coord, int offset)
        {
            return new Cubic(coord.X - offset, coord.Y - offset, coord.Z - offset);
        }

        public static Cubic operator *(Cubic coord, int offset)
        {
            return new Cubic(coord.X * offset, coord.Y * offset, coord.Z * offset);
        }

        public static Cubic operator *(Cubic coord, float delta)
        {
            return new Cubic(coord.X * delta, coord.Y * delta, coord.Z * delta);
        }

        public bool IsValid()
        {
            return X + Y + Z == 0;
        }

        public Cubic RotateToRight()
        {
            var x = -Y;
            var y = -Z;
            var z = -X;
            return new Cubic(x, y, z);
        }

        public Cubic RotateToRight(int times)
        {
            var cur = this;
            for (var i = 0; i < times; i++)
            {
                cur = cur.RotateToRight();
            }

            return cur;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            var other = (Cubic) obj;
            return (X, Y, Z).Equals((other.X, other.Y, other.Z));
        }

        public override int GetHashCode()
        {
            return (X, Y, Z).GetHashCode();
        }

        public override string ToString()
        {
            return IsValid() ? "C-[Invalid]" : $"C-[{X}:{Y}:{Z}]";
        }
    }
}
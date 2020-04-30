using System;
using System.Collections.Generic;
using HexagonalLib.Coordinates;

namespace HexagonalLib
{
    /// <summary>
    /// Represent geometry logic for infinity hexagonal grid
    /// </summary>
    public readonly partial struct HexagonalGrid
    {
        /// <summary>
        /// Total count of edges in one Hex
        /// </summary>
        public const int EdgesCount = 6;

        public static float Sqrt3 { get; } = (float) Math.Sqrt(3);

        /// <summary>
        /// Inscribed radius of the hex
        /// </summary>
        public readonly float InscribedRadius;

        /// <summary>
        /// Outscribed radius of hex
        /// </summary>
        public readonly float OutscribedRadius;

        /// <summary>
        /// Edge length of hex
        /// </summary>
        public float EdgeLength => OutscribedRadius;

        public float InscribedDiameter => InscribedRadius * 2;

        /// <summary>
        /// Orientation and layout of this grid
        /// </summary>
        public readonly HexagonalGridType Type;

        /// <summary>
        /// Offset between hex and its right-side neighbour on X axis
        /// </summary>
        public float XOffset
        {
            get
            {
                switch (Type)
                {
                    case HexagonalGridType.FlatOdd:
                    case HexagonalGridType.FlatEven:
                        return EdgeLength * 1.5f;
                    case HexagonalGridType.PointyOdd:
                    case HexagonalGridType.PointyEven:
                        return InscribedRadius * 2.0f;
                    default:
                        throw new HexagonalException($"Can't get {nameof(XOffset)} with unexpected {nameof(Type)}", this);
                }
            }
        }

        /// <summary>
        /// Offset between hex and its up-side neighbour on Y axis
        /// </summary>
        public float YOffset
        {
            get
            {
                switch (Type)
                {
                    case HexagonalGridType.FlatOdd:
                    case HexagonalGridType.FlatEven:
                        return InscribedRadius * 2.0f;
                    case HexagonalGridType.PointyOdd:
                    case HexagonalGridType.PointyEven:
                        return EdgeLength * 1.5f;
                    default:
                        throw new HexagonalException($"Can't get {nameof(YOffset)} with unexpected {nameof(Type)}", this);
                }
            }
        }

        /// <summary>
        /// The angle between the centers of any hex and its first neighbor relative to the vector (0, 1) clockwise
        /// </summary>
        /// <exception cref="HexagonalException"></exception>
        public float AngleToFirstNeighbor
        {
            get
            {
                switch (Type)
                {
                    case HexagonalGridType.FlatOdd:
                    case HexagonalGridType.FlatEven:
                        return 30.0f;
                    case HexagonalGridType.PointyOdd:
                    case HexagonalGridType.PointyEven:
                        return 0.0f;
                    default:
                        throw new HexagonalException($"Can't get {nameof(AngleToFirstNeighbor)} with unexpected {nameof(Type)}", this);
                }
            }
        }

        /// <summary>
        /// Base constructor for hexagonal grid
        /// </summary>
        /// <param name="type">Orientation and layout of the grid</param>
        /// <param name="radius">Inscribed radius</param>
        public HexagonalGrid(HexagonalGridType type, float radius)
        {
            Type = type;
            InscribedRadius = radius;
            OutscribedRadius = (float) (radius / Math.Cos(Math.PI / EdgesCount));
        }

        #region ToOffset

        /// <summary>
        /// Convert cube coordinate to offset
        /// </summary>
        public Offset ToOffset(Cubic coord)
        {
            switch (Type)
            {
                case HexagonalGridType.FlatOdd:
                {
                    var col = coord.X;
                    var row = coord.Z + (coord.X - (coord.X & 1)) / 2;
                    return new Offset(col, row);
                }
                case HexagonalGridType.FlatEven:
                {
                    var col = coord.X;
                    var row = coord.Z + (coord.X + (coord.X & 1)) / 2;
                    return new Offset(col, row);
                }
                case HexagonalGridType.PointyOdd:
                {
                    var col = coord.X + (coord.Z - (coord.Z & 1)) / 2;
                    var row = coord.Z;
                    return new Offset(col, row);
                }
                case HexagonalGridType.PointyEven:
                {
                    var col = coord.X + (coord.Z + (coord.Z & 1)) / 2;
                    var row = coord.Z;
                    return new Offset(col, row);
                }
                default:
                    throw new HexagonalException($"{nameof(ToOffset)} failed with unexpected {nameof(Type)}", this, (nameof(coord), coord));
            }
        }

        /// <summary>
        /// Convert axial coordinate to offset
        /// </summary>
        public Offset ToOffset(Axial axial)
        {
            return ToOffset(ToCube(axial));
        }

        /// <summary>
        /// Convert point to offset coordinate
        /// </summary>
        public Offset ToOffset(float x, float y)
        {
            return ToOffset(ToCubic(x, y));
        }

        /// <summary>
        /// Convert point to offset coordinate
        /// </summary>
        public Offset ToOffset((float x, float y) point)
        {
            return ToOffset(ToCubic(point.x, point.y));
        }

        #endregion

        #region ToAxial

        /// <summary>
        /// Convert cube coordinate to axial
        /// </summary>
        public Axial ToAxial(Cubic cubic)
        {
            return new Axial(cubic.X, cubic.Z);
        }

        /// <summary>
        /// Convert offset coordinate to axial
        /// </summary>
        public Axial ToAxial(Offset offset)
        {
            return ToAxial(ToCube(offset));
        }

        /// <summary>
        /// Convert point to axial coordinate
        /// </summary>
        public Axial ToAxial((float x, float y) point)
        {
            return ToAxial(ToCubic(point.x, point.y));
        }

        /// <summary>
        /// Convert point to axial coordinate
        /// </summary>
        public Axial ToAxial(float x, float y)
        {
            return ToAxial(ToCubic(x, y));
        }

        #endregion

        #region ToCubic

        /// <summary>
        /// Convert offset coordinate to cube
        /// </summary>
        public Cubic ToCube(Offset coord)
        {
            switch (Type)
            {
                case HexagonalGridType.FlatOdd:
                {
                    var x = coord.X;
                    var z = coord.Y - (coord.X - (coord.X & 1)) / 2;
                    var y = -x - z;
                    return new Cubic(x, y, z);
                }
                case HexagonalGridType.FlatEven:
                {
                    var x = coord.X;
                    var z = coord.Y - (coord.X + (coord.X & 1)) / 2;
                    var y = -x - z;
                    return new Cubic(x, y, z);
                }
                case HexagonalGridType.PointyOdd:
                {
                    var x = coord.X - (coord.Y - (coord.Y & 1)) / 2;
                    var z = coord.Y;
                    var y = -x - z;
                    return new Cubic(x, y, z);
                }
                case HexagonalGridType.PointyEven:
                {
                    var x = coord.X - (coord.Y + (coord.Y & 1)) / 2;
                    var z = coord.Y;
                    var y = -x - z;
                    return new Cubic(x, y, z);
                }
                default:
                    throw new HexagonalException($"{nameof(ToCube)} failed with unexpected {nameof(Type)}", this, (nameof(coord), coord));
            }
        }

        /// <summary>
        /// Convert axial coordinate to cube
        /// </summary>
        public Cubic ToCube(Axial axial)
        {
            return new Cubic(axial.Q, -axial.Q - axial.R, axial.R);
        }

        /// <summary>
        /// Convert point to cubic coordinate
        /// </summary>
        public Cubic ToCubic((float x, float y) point)
        {
            return ToCubic(point.x, point.y);
        }

        /// <summary>
        /// Convert point to cubic coordinate
        /// </summary>
        public Cubic ToCubic(float x, float y)
        {
            switch (Type)
            {
                case HexagonalGridType.FlatOdd:
                case HexagonalGridType.FlatEven:
                {
                    var q = x * 2.0f / 3.0f / OutscribedRadius;
                    var r = (-x / 3.0f + Sqrt3 / 3.0f * y) / OutscribedRadius;
                    return new Cubic(q, -q - r, r);
                }
                case HexagonalGridType.PointyOdd:
                case HexagonalGridType.PointyEven:
                {
                    var q = (x * Sqrt3 / 3.0f - y / 3.0f) / OutscribedRadius;
                    var r = y * 2.0f / 3.0f / OutscribedRadius;
                    return new Cubic(q, -q - r, r);
                }
                default:
                    throw new HexagonalException($"{nameof(ToCubic)} failed with unexpected {nameof(Type)}", this, (nameof(x), x), (nameof(y), y));
            }
        }

        #endregion

        #region ToPoint2

        /// <summary>
        /// Convert hex based on its offset coordinate to it center position in 2d space
        /// </summary>
        public (float x, float y) ToPoint2(Offset coord)
        {
            switch (Type)
            {
                case HexagonalGridType.FlatOdd:
                case HexagonalGridType.FlatEven:
                {
                    float x = OutscribedRadius * 1.5f * coord.X;
                    float y = InscribedDiameter * (coord.Y + 0.5f * (coord.X & 1));
                    return (x, y);
                }
                case HexagonalGridType.PointyOdd:
                case HexagonalGridType.PointyEven:
                {
                    float x = InscribedDiameter * (coord.X - 0.5f * (coord.Y & 1));
                    float y = OutscribedRadius * 1.5f * coord.Y;
                    return (x, y);
                }
                default:
                    throw new HexagonalException($"{nameof(ToPoint2)} failed with unexpected {nameof(Type)}", this, (nameof(coord), coord));
            }
        }

        /// <summary>
        /// Convert hex based on its axial coordinate to it center position in 2d space
        /// </summary>
        public (float x, float y) ToPoint2(Axial coord)
        {
            switch (Type)
            {
                case HexagonalGridType.FlatOdd:
                case HexagonalGridType.FlatEven:
                {
                    float x = OutscribedRadius * 1.5f * coord.Q;
                    float y = InscribedDiameter * (coord.R + coord.Q * 0.5f);
                    return (x, y);
                }
                case HexagonalGridType.PointyOdd:
                case HexagonalGridType.PointyEven:
                {
                    float x = InscribedDiameter * (coord.Q + coord.R * 0.5f);
                    float y = OutscribedRadius * 1.5f * coord.R;
                    return (x, y);
                }
                default:
                    throw new HexagonalException($"{nameof(ToPoint2)} failed with unexpected {nameof(Type)}", this, (nameof(coord), coord));
            }
        }

        /// <summary>
        /// Convert hex based on its cubic coordinate to it center position in 2d space
        /// </summary>
        public (float x, float y) ToPoint2(Cubic coord)
        {
            return ToPoint2(ToAxial(coord));
        }

        #endregion

        /// <summary>
        /// Checks whether the two hexes are neighbors or no
        /// </summary>
        public bool IsNeighbors(Offset index1, Offset index2)
        {
            foreach (var offset in GetNeighborsOffsets(index1))
            {
                if (index1 + offset == index2)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a ring with a radius of <see cref="radius"/> hexes around the given <see cref="center"/>.
        /// </summary>
        public IEnumerable<Offset> GetNeighborsRing(Offset center, int radius)
        {
            if (radius == 0)
            {
                yield return center;
                yield break;
            }

            for (var i = 0; i < radius; i++)
            {
                center = GetNeighbor(center, 4);
            }

            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < radius; j++)
                {
                    yield return center;
                    center = GetNeighbor(center, i);
                }
            }
        }

        public IEnumerable<Offset> GetNeighborsAround(Offset center, int radius)
        {
            for (var i = 0; i < radius; i++)
            {
                foreach (var hex in GetNeighborsRing(center, i))
                {
                    yield return hex;
                }
            }
        }

        /// <summary>
        /// Return all neighbors of the hex
        /// </summary>
        public IEnumerable<Offset> GetNeighbors(Offset hex)
        {
            foreach (var offset in GetNeighborsOffsets(hex))
            {
                yield return offset + hex;
            }
        }

        /// <summary>
        /// Return all neighbors offsets of the hex
        /// </summary>
        public IReadOnlyList<Offset> GetNeighborsOffsets(Offset coord)
        {
            switch (Type)
            {
                case HexagonalGridType.FlatOdd:
                    return coord.X % 2 == 0 ? _flatOddNeighbors : _flatEvenNeighbors;
                case HexagonalGridType.FlatEven:
                    return coord.X % 2 == 1 ? _flatOddNeighbors : _flatEvenNeighbors;
                case HexagonalGridType.PointyOdd:
                    return coord.Y % 2 == 0 ? _pointyOddNeighbors : _pointyEvenNeighbors;
                case HexagonalGridType.PointyEven:
                    return coord.Y % 2 == 1 ? _pointyOddNeighbors : _pointyEvenNeighbors;
                default:
                    throw new HexagonalException($"{nameof(GetNeighborsOffsets)} failed with unexpected {nameof(Type)}", this, (nameof(coord), coord));
            }
        }

        /// <summary>
        /// Returns the neighbor at the specified index.
        /// </summary>
        public Offset GetNeighbor(Offset coord, int neighborIndex)
        {
            neighborIndex = (EdgesCount + neighborIndex % EdgesCount) % EdgesCount;
            var dir = GetNeighborsOffsets(coord)[neighborIndex];
            return coord + dir;
        }

        /// <summary>
        /// Returns the direction to the specified neighbor
        /// </summary>
        public byte GetBypassIndex(Offset center, Offset neighbor)
        {
            var offsets = GetNeighborsOffsets(center);
            for (byte i = 0; i < offsets.Count; i++)
            {
                if (center + offsets[i] == neighbor)
                {
                    return i;
                }
            }

            throw new HexagonalException($"Can't find bypass index", this, (nameof(center), center), (nameof(neighbor), neighbor));
        }

        /// <summary>
        /// Manhattan distance between two hexes
        /// </summary>
        public int CubeDistance(Offset h1, Offset h2)
        {
            var cubicFrom = ToCube(h1);
            var cubicTo = ToCube(h2);
            return CubeDistance(cubicFrom, cubicTo);
        }

        /// <summary>
        /// Manhattan distance between two hexes
        /// </summary>
        public static int CubeDistance(Cubic h1, Cubic h2)
        {
            return (Math.Abs(h1.X - h2.X) + Math.Abs(h1.Y - h2.Y) + Math.Abs(h1.Z - h2.Z)) / 2;
        }

        private static readonly List<Offset> _pointyOddNeighbors = new List<Offset>
        {
            new Offset(0, +1),
            new Offset(+1, 0),
            new Offset(0, -1),
            new Offset(-1, -1),
            new Offset(-1, 0),
            new Offset(-1, +1),
        };

        private static readonly List<Offset> _pointyEvenNeighbors = new List<Offset>
        {
            new Offset(+1, +1),
            new Offset(+1, 0),
            new Offset(+1, -1),
            new Offset(0, -1),
            new Offset(-1, 0),
            new Offset(0, +1),
        };

        private static readonly List<Offset> _flatOddNeighbors = new List<Offset>
        {
            new Offset(0, +1),
            new Offset(+1, 0),
            new Offset(+1, -1),
            new Offset(0, -1),
            new Offset(-1, -1),
            new Offset(-1, 0),
        };

        private static readonly List<Offset> _flatEvenNeighbors = new List<Offset>
        {
            new Offset(0, +1),
            new Offset(+1, +1),
            new Offset(+1, 0),
            new Offset(0, -1),
            new Offset(-1, 0),
            new Offset(-1, +1),
        };
    }
}
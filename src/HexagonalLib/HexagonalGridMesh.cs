using System;
using System.Collections.Generic;
using HexagonalLib.Coordinates;

namespace HexagonalLib
{
    public readonly partial struct HexagonalGrid
    {
        /// <summary>
        /// Calculate count of vertices and indices needed for build mesh.
        /// </summary>
        /// <param name="hexesCount">Count on hexes in mesh</param>
        /// <param name="subdivide">Count of triangles splits for each hex</param>
        public (int VerticesCount, int IndicesCount) GetMeshData(int hexesCount, int subdivide)
        {
            // Calculate number of vertices we need to generate
            // numVertices = 1 + Σ {S, i = 1} (i * 6)
            var numVertices = 1;

            // Calculate number of indices we need to generate
            // numIndices = Σ {S, i = 1} (36 * i - 18)
            var numIndices = 0;

            for (int i = 1; i <= subdivide; i++)
            {
                numVertices += i * 6;
                numIndices += 36 * i - 18;
            }

            return (numVertices * hexesCount, numIndices * hexesCount);
        }

        /// <summary>
        /// Generates a mesh for list of hex. The generation algorithm is taken from the site:
        /// </summary>
        public void CreateMesh(IEnumerable<Offset> hexes, int subdivide, Action<int, (float X, float Y)> setVertex, Action<int, int> setIndex)
        {
            CreateMesh(hexes, subdivide, setVertex, setIndex, ToPoint2);
        }

        /// <summary>
        /// Generates a mesh for list of hex. The generation algorithm is taken from the site:
        /// </summary>
        public void CreateMesh(IEnumerable<Axial> hexes, int subdivide, Action<int, (float X, float Y)> setVertex, Action<int, int> setIndex)
        {
            CreateMesh(hexes, subdivide, setVertex, setIndex, ToPoint2);
        }

        /// <summary>
        /// Generates a mesh for list of hex. The generation algorithm is taken from the site:
        /// </summary>
        public void CreateMesh(IEnumerable<Cubic> hexes, int subdivide, Action<int, (float X, float Y)> setVertex, Action<int, int> setIndex)
        {
            CreateMesh(hexes, subdivide, setVertex, setIndex, ToPoint2);
        }

        /// <summary>
        /// Generates a mesh for list of hex. The generation algorithm is taken from the site:
        /// http://www.voidinspace.com/2014/07/project-twa-part-1-generating-a-hexagonal-tile-and-its-triangular-grid/
        /// </summary>
        private void CreateMesh<T>(IEnumerable<T> hexes, int subdivide, Action<int, (float X, float Y)> setVertex, Action<int, int> setIndex, Func<T, (float X, float Y)> toPoint)
        {
            var vertex = 0;
            var index = 0;

            var data = GetMeshData(1, subdivide);

            foreach (var hex in hexes)
            {
                var localVertex = vertex;
                var localIndex = index;
                var center = toPoint(hex);

                void setVertexLocal(int i, (float X, float Y) currentVertex)
                {
                    var (x, y) = currentVertex;
                    var shifted = (x + center.X, y + center.Y);
                    setVertex(localVertex + i, shifted);
                }

                void setIndexLocal(int i, int currentIndex) => setIndex(localIndex + i, localVertex + currentIndex);

                CreateMesh(subdivide, setVertexLocal, setIndexLocal);

                vertex += data.VerticesCount;
                index += data.IndicesCount;
            }
        }

        /// <summary>
        /// Generates a mesh for one hex. The generation algorithm is taken from the site:
        /// http://www.voidinspace.com/2014/07/project-twa-part-1-generating-a-hexagonal-tile-and-its-triangular-grid/
        /// </summary>
        public void CreateMesh(int subdivide, Action<int, (float X, float Y)> setVertex, Action<int, int> setIndex)
        {
            // We'll need those later to generate our x, z coordinates
            var radius = InscribedRadius / (float) Math.Cos(Math.PI / 6);
            var sin60 = (float) Math.Sin(Math.PI / 3);
            var invTan60 = 1.0f / (float) Math.Tan(Math.PI / 3.0f);
            var rdq = radius / subdivide;

            var verticesIndex = 0;
            var indicesIndex = 0;

            // We'll need those to calculate our indices
            var currentNumPoints = 0;
            var prevRowNumPoints = 0;

            // [colMin, colMax]
            var npCol0 = 2 * subdivide + 1;
            var colMin = -subdivide;
            var colMax = subdivide;

            //
            // Now let's generate the grid
            // First we'll iterate through the Columns, starting from the bottom (fA)
            for (var itC = colMin; itC <= colMax; itC++)
            {
                // Calculate z for this row
                // That's the same for each point in this column
                var x = sin60 * rdq * itC;

                // Calculate how many points (z values) we need to generate on for this column
                var npColI = npCol0 - Math.Abs(itC);

                // [rowMin, rowMax]
                var rowMin = -subdivide;
                if (itC < 0)
                {
                    rowMin += Math.Abs(itC);
                }

                var rowMax = rowMin + npColI - 1;

                // We need this for the indices
                currentNumPoints += npColI;

                // Iterate through the Rows (fB)
                for (var itR = rowMin; itR <= rowMax; itR++)
                {
                    // Calculate z
                    var z = invTan60 * x + rdq * itR;
                    setVertex(verticesIndex, (x, z).Rotate(AngleToFirstNeighbor));

                    // Indices
                    // From each point we'll try to create triangles left and right
                    if (verticesIndex < (currentNumPoints - 1))
                    {
                        // Triangles left from this column
                        if (itC >= colMin && itC < colMax)
                        {
                            // To get the point above
                            var padLeft = 0;
                            if (itC < 0)
                            {
                                padLeft = 1;
                            }

                            setIndex(indicesIndex++, verticesIndex + npColI + padLeft);
                            setIndex(indicesIndex++, verticesIndex + 1);
                            setIndex(indicesIndex++, verticesIndex);
                        }

                        // Triangles right from this column
                        if (itC > colMin && itC <= colMax)
                        {
                            // To get point bellow
                            var padRight = 0;
                            if (itC > 0)
                            {
                                padRight = 1;
                            }

                            setIndex(indicesIndex++, verticesIndex - prevRowNumPoints + padRight);
                            setIndex(indicesIndex++, verticesIndex);
                            setIndex(indicesIndex++, verticesIndex + 1);
                        }
                    }

                    // Next vertex...
                    verticesIndex++;
                }

                // Store the previous row number of points, used to calculate the index buffer
                prevRowNumPoints = npColI;
            }
        }
    }
}
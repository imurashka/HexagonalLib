using HexagonalLib.Coordinates;
using NUnit.Framework;
using static System.Math;

namespace HexagonalLib.Tests
{
    [TestFixture(TestOf = typeof(HexagonalGrid))]
    public class HexagonalGridTests
    {
        private float InscribedRadius => 0.5f;
        private float DescribedRadius => (float) (InscribedRadius / Cos(PI / HexagonalGrid.EdgesCount));

        [Test(Author = "Ivan Murashka", Description = "Check initial properties values for flat grids after creation")]
        [TestCase(HexagonalGridType.FlatEven)]
        [TestCase(HexagonalGridType.FlatOdd)]
        public void FlatPropertiesTest(HexagonalGridType type)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            Assert.AreEqual(grid.InscribedRadius, InscribedRadius);
            Assert.AreEqual(grid.DescribedRadius, DescribedRadius);
            Assert.AreEqual(grid.InscribedDiameter, InscribedRadius * 2);
            Assert.AreEqual(grid.DescribedDiameter, DescribedRadius * 2);
            Assert.AreEqual(grid.HorizontalOffset, DescribedRadius * 1.5f);
            Assert.AreEqual(grid.VerticalOffset, InscribedRadius * 2.0f);
            Assert.AreEqual(grid.Side, DescribedRadius);
            Assert.AreEqual(grid.AngleToFirstNeighbor, 30.0f);
        }

        [Test(Author = "Ivan Murashka", Description = "Check initial properties values for pointy grids after creation")]
        [TestCase(HexagonalGridType.PointyEven)]
        [TestCase(HexagonalGridType.PointyOdd)]
        public void PointyPropertiesTest(HexagonalGridType type)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            Assert.AreEqual(grid.InscribedRadius, InscribedRadius);
            Assert.AreEqual(grid.DescribedRadius, DescribedRadius);
            Assert.AreEqual(grid.InscribedDiameter, InscribedRadius * 2);
            Assert.AreEqual(grid.DescribedDiameter, DescribedRadius * 2);
            Assert.AreEqual(grid.HorizontalOffset, InscribedRadius * 2.0f);
            Assert.AreEqual(grid.VerticalOffset, DescribedRadius * 1.5f);
            Assert.AreEqual(grid.Side, DescribedRadius);
            Assert.AreEqual(grid.AngleToFirstNeighbor, 0.0f);
        }

        [Test(Author = "Ivan Murashka", Description = "Check coordinate conversion from Offset")]
        public void CoordinateConversionTest(
            [Values] HexagonalGridType type,
            [Values(-13, -8, 0, 15, 22)] int offsetX,
            [Values(-13, -8, 0, 15, 22)] int offsetY)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            var offset = new Offset(offsetX, offsetY);
            var axial = grid.ToAxial(offset);
            var cubic = grid.ToCubic(offset);
            Assert.IsTrue(cubic.IsValid(), $"Invalid cubic coordinate: {cubic.X}-{cubic.Y}-{cubic.Z}");
            Assert.AreEqual(offset, grid.ToOffset(axial));
            Assert.AreEqual(offset, grid.ToOffset(cubic));
            Assert.AreEqual(axial, grid.ToAxial(offset));
            Assert.AreEqual(axial, grid.ToAxial(cubic));
            Assert.AreEqual(cubic, grid.ToCubic(offset));
            Assert.AreEqual(cubic, grid.ToCubic(axial));
        }

        [Test(Author = "Ivan Murashka", Description = "Check conversion to Point2 from Offset")]
        public void PointConversionTest(
            [Values] HexagonalGridType type,
            [Values(-13, -8, 0, 15, 22)] int offsetX,
            [Values(-13, -8, 0, 15, 22)] int offsetY)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            var offset = new Offset(offsetX, offsetY);
            var axial = grid.ToAxial(offset);
            var cubic = grid.ToCubic(offset);

            var fromOffset = grid.ToPoint2(offset);
            var fromAxial = grid.ToPoint2(axial);
            var fromCubic = grid.ToPoint2(cubic);

            Assert.IsTrue(fromOffset.SimilarTo(fromAxial), $"Expected: {fromAxial.X}:{fromAxial.Y}; Actual: {fromOffset.X}:{fromOffset.Y}");
            Assert.IsTrue(fromOffset.SimilarTo(fromCubic), $"Expected: {fromCubic.X}:{fromCubic.Y}; Actual: {fromOffset.X}:{fromOffset.Y}");

            Assert.AreEqual(offset, grid.ToOffset(fromOffset));
            Assert.AreEqual(axial, grid.ToAxial(fromAxial));
            Assert.AreEqual(cubic, grid.ToCubic(fromCubic));
        }

        [Test(Author = "Ivan Murashka", Description = "Check IsNeighbor methods for all coordinates types")]
        public void IsNeighborTest(
            [Values] HexagonalGridType type,
            [Values(-13, -8, 0, 15, 22)] int offsetX,
            [Values(-13, -8, 0, 15, 22)] int offsetY,
            [Values(-1, 0, 1, 2, 3, 4, 5, 6)] int neighborIndex)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            var offset = new Offset(offsetX, offsetY);
            var axial = grid.ToAxial(offset);
            var cubic = grid.ToCubic(offset);
            Assert.IsTrue(cubic.IsValid(), $"Invalid cubic coordinate: {cubic.X}-{cubic.Y}-{cubic.Z}");

            var oNeighbor = grid.GetNeighbor(offset, neighborIndex);
            var aNeighbor = grid.GetNeighbor(axial, neighborIndex);
            var cNeighbor = grid.GetNeighbor(cubic, neighborIndex);

            Assert.IsTrue(cNeighbor.IsValid(), $"Invalid cubic coordinate: {cNeighbor.X}-{cNeighbor.Y}-{cNeighbor.Z}");
            Assert.IsTrue(grid.IsNeighbors(offset, oNeighbor), $"Neighbor1={offset}; Neighbor2={oNeighbor}; Index={neighborIndex};");
            Assert.IsTrue(grid.IsNeighbors(axial, aNeighbor), $"Neighbor1={axial}; Neighbor2={aNeighbor}; Index={neighborIndex};");
            Assert.IsTrue(grid.IsNeighbors(cubic, cNeighbor), $"Neighbor1={cubic}; Neighbor2={cNeighbor}; Index={neighborIndex};");
        }

        [Test(Author = "Ivan Murashka", Description = "Check neighbors order for all coordinates types")]
        public void NeighborsOrderTest(
            [Values] HexagonalGridType type,
            [Values(-13, -8, 0, 15, 22)] int offsetX,
            [Values(-13, -8, 0, 15, 22)] int offsetY,
            [Values(-1, 0, 1, 2, 3, 4, 5, 6)] int neighborIndex)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            var offset = new Offset(offsetX, offsetY);
            var axial = grid.ToAxial(offset);
            var cubic = grid.ToCubic(offset);

            var oNeighbor = grid.GetNeighbor(offset, neighborIndex);
            var aNeighbor = grid.GetNeighbor(axial, neighborIndex);
            var cNeighbor = grid.GetNeighbor(cubic, neighborIndex);

            Assert.IsTrue(cNeighbor.IsValid(), $"Invalid cubic coordinate: {cNeighbor.X}-{cNeighbor.Y}-{cNeighbor.Z}");

            var fromAxial = grid.ToOffset(aNeighbor);
            var fromCubic = grid.ToOffset(cNeighbor);

            Assert.AreEqual(oNeighbor, fromAxial, $"Center=({offset} - {axial}); Current=({oNeighbor} - {aNeighbor}); Index={neighborIndex};");
            Assert.AreEqual(oNeighbor, fromCubic, $"Center=({offset} - {cubic}); Current=({oNeighbor} - {cNeighbor}); Index={neighborIndex};");
        }
    }
}
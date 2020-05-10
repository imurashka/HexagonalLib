using HexagonalLib.Coordinates;
using NUnit.Framework;

namespace HexagonalLib.Tests
{
    [TestFixture]
    public class HexagonalGridTests
    {
        private float InscribedRadius => 0.5f;
        
        [Test(Author = "Ivan Murashka", Description = "Check coordinate conversion from Offset to Unity vectors")]
        public void Vector2ConversionTest(
            [Values] HexagonalGridType type,
            [Values(-13, -8, 0, 15, 22)] int offsetX,
            [Values(-13, -8, 0, 15, 22)] int offsetY)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            var offset = new Offset(offsetX, offsetY);
            var axial = grid.ToAxial(offset);
            var cubic = grid.ToCubic(offset);

            var fromOffset = grid.ToVector2(offset);
            var fromAxial = grid.ToVector2(axial);
            var fromCubic = grid.ToVector2(cubic);

            Assert.IsTrue(fromOffset.SimilarTo(fromAxial), $"Expected: {fromAxial}; Actual: {fromOffset}");
            Assert.IsTrue(fromOffset.SimilarTo(fromCubic), $"Expected: {fromCubic}; Actual: {fromOffset}");

            Assert.AreEqual(offset, grid.ToOffset(fromOffset));
            Assert.AreEqual(axial, grid.ToAxial(fromAxial));
            Assert.AreEqual(cubic, grid.ToCubic(fromCubic));
        }
        
        [Test(Author = "Ivan Murashka", Description = "Check coordinate conversion from Offset to Unity vectors")]
        public void Vector3ConversionTest(
            [Values] HexagonalGridType type,
            [Values(-13, -8, 0, 15, 22)] int offsetX,
            [Values(-13, -8, 0, 15, 22)] int offsetY)
        {
            var grid = new HexagonalGrid(type, InscribedRadius);
            var offset = new Offset(offsetX, offsetY);
            var axial = grid.ToAxial(offset);
            var cubic = grid.ToCubic(offset);

            var fromOffset = grid.ToVector3(offset);
            var fromAxial = grid.ToVector3(axial);
            var fromCubic = grid.ToVector3(cubic);

            Assert.IsTrue(fromOffset.SimilarTo(fromAxial), $"Expected: {fromAxial}; Actual: {fromOffset}");
            Assert.IsTrue(fromOffset.SimilarTo(fromCubic), $"Expected: {fromCubic}; Actual: {fromOffset}");

            Assert.AreEqual(offset, grid.ToOffset(fromOffset));
            Assert.AreEqual(axial, grid.ToAxial(fromAxial));
            Assert.AreEqual(cubic, grid.ToCubic(fromCubic));
        }
    }
}